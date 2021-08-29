/**
 * Panzoom for panning and zooming elements using CSS transforms
 * Copyright Timmy Willison and other contributors
 * https://github.com/timmywil/panzoom/blob/master/MIT-License.txt
 */
(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
    typeof define === 'function' && define.amd ? define(factory) :
    (global = global || self, global.Panzoom = factory());
}(this, (function () { 'use strict';

    /*! *****************************************************************************
    Copyright (c) Microsoft Corporation.

    Permission to use, copy, modify, and/or distribute this software for any
    purpose with or without fee is hereby granted.

    THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH
    REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY
    AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT,
    INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM
    LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR
    OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR
    PERFORMANCE OF THIS SOFTWARE.
    ***************************************************************************** */

    var __assign = function() {
        __assign = Object.assign || function __assign(t) {
            for (var s, i = 1, n = arguments.length; i < n; i++) {
                s = arguments[i];
                for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p)) t[p] = s[p];
            }
            return t;
        };
        return __assign.apply(this, arguments);
    };

    /* eslint-disable no-var */
    // Support: IE11 only
    if (window.NodeList && !NodeList.prototype.forEach) {
      NodeList.prototype.forEach = Array.prototype.forEach;
    }
    // Support: IE11 only
    // CustomEvent is an object instead of a constructor
    if (typeof window.CustomEvent !== 'function') {
      window.CustomEvent = function CustomEvent(event, params) {
        params = params || { bubbles: false, cancelable: false, detail: null };
        var evt = document.createEvent('CustomEvent');
        evt.initCustomEvent(event, params.bubbles, params.cancelable, params.detail);
        return evt
      };
    }

    /**
     * Utilites for working with multiple pointer events
     */
    function findEventIndex(pointers, event) {
        var i = pointers.length;
        while (i--) {
            if (pointers[i].pointerId === event.pointerId) {
                return i;
            }
        }
        return -1;
    }
    function addPointer(pointers, event) {
        var i;
        // Add touches if applicable
        if (event.touches) {
            i = 0;
            for (var _i = 0, _a = event.touches; _i < _a.length; _i++) {
                var touch = _a[_i];
                touch.pointerId = i++;
                addPointer(pointers, touch);
            }
            return;
        }
        i = findEventIndex(pointers, event);
        // Update if already present
        if (i > -1) {
            pointers.splice(i, 1);
        }
        pointers.push(event);
    }
    function removePointer(pointers, event) {
        // Add touches if applicable
        if (event.touches) {
            // Remove all touches
            while (pointers.length) {
                pointers.pop();
            }
            return;
        }
        var i = findEventIndex(pointers, event);
        if (i > -1) {
            pointers.splice(i, 1);
        }
    }
    /**
     * Calculates a center point between
     * the given pointer events, for panning
     * with multiple pointers.
     */
    function getMiddle(pointers) {
        // Copy to avoid changing by reference
        pointers = pointers.slice(0);
        var event1 = pointers.pop();
        var event2;
        while ((event2 = pointers.pop())) {
            event1 = {
                clientX: (event2.clientX - event1.clientX) / 2 + event1.clientX,
                clientY: (event2.clientY - event1.clientY) / 2 + event1.clientY
            };
        }
        return event1;
    }
    /**
     * Calculates the distance between two points
     * for pinch zooming.
     * Limits to the first 2
     */
    function getDistance(pointers) {
        if (pointers.length < 2) {
            return 0;
        }
        var event1 = pointers[0];
        var event2 = pointers[1];
        return Math.sqrt(Math.pow(Math.abs(event2.clientX - event1.clientX), 2) +
            Math.pow(Math.abs(event2.clientY - event1.clientY), 2));
    }

    var events;
    if (typeof window.PointerEvent === 'function') {
        events = {
            down: 'pointerdown',
            move: 'pointermove',
            up: 'pointerup pointerleave pointercancel'
        };
    }
    else if (typeof window.TouchEvent === 'function') {
        events = {
            down: 'touchstart',
            move: 'touchmove',
            up: 'touchend touchcancel'
        };
    }
    else {
        events = {
            down: 'mousedown',
            move: 'mousemove',
            up: 'mouseup mouseleave'
        };
    }
    function onPointer(event, elem, handler, eventOpts) {
        events[event].split(' ').forEach(function (name) {
            elem.addEventListener(name, handler, eventOpts);
        });
    }
    function destroyPointer(event, elem, handler) {
        events[event].split(' ').forEach(function (name) {
            elem.removeEventListener(name, handler);
        });
    }

    var isIE = !!document.documentMode;
    /**
     * Proper prefixing for cross-browser compatibility
     */
    var divStyle = document.createElement('div').style;
    var prefixes = ['webkit', 'moz', 'ms'];
    var prefixCache = {};
    function getPrefixedName(name) {
        if (prefixCache[name]) {
            return prefixCache[name];
        }
        if (name in divStyle) {
            return (prefixCache[name] = name);
        }
        var capName = name[0].toUpperCase() + name.slice(1);
        var i = prefixes.length;
        while (i--) {
            var prefixedName = "" + prefixes[i] + capName;
            if (prefixedName in divStyle) {
                return (prefixCache[name] = prefixedName);
            }
        }
    }
    /**
     * Gets a style value expected to be a number
     */
    function getCSSNum(name, style) {
        return parseFloat(style[getPrefixedName(name)]) || 0;
    }
    function getBoxStyle(elem, name, style) {
        if (style === void 0) { style = window.getComputedStyle(elem); }
        // Support: FF 68+
        // Firefox requires specificity for border
        var suffix = name === 'border' ? 'Width' : '';
        return {
            left: getCSSNum(name + "Left" + suffix, style),
            right: getCSSNum(name + "Right" + suffix, style),
            top: getCSSNum(name + "Top" + suffix, style),
            bottom: getCSSNum(name + "Bottom" + suffix, style)
        };
    }
    /**
     * Set a style using the properly prefixed name
     */
    function setStyle(elem, name, value) {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        elem.style[getPrefixedName(name)] = value;
    }
    /**
     * Constructs the transition from panzoom options
     * and takes care of prefixing the transition and transform
     */
    function setTransition(elem, options) {
        var transform = getPrefixedName('transform');
        setStyle(elem, 'transition', transform + " " + options.duration + "ms " + options.easing);
    }
    /**
     * Set the transform using the proper prefix
     */
    function setTransform(elem, _a, _options) {
        var x = _a.x, y = _a.y, scale = _a.scale, isSVG = _a.isSVG;
        setStyle(elem, 'transform', "scale(" + scale + ") translate(" + x + "px, " + y + "px)");
        if (isSVG && isIE) {
            var matrixValue = window.getComputedStyle(elem).getPropertyValue('transform');
            elem.setAttribute('transform', matrixValue);
        }
    }
    /**
     * Dimensions used in containment and focal point zooming
     */
    function getDimensions(elem) {
        var parent = elem.parentNode;
        var style = window.getComputedStyle(elem);
        var parentStyle = window.getComputedStyle(parent);
        var rectElem = elem.getBoundingClientRect();
        var rectParent = parent.getBoundingClientRect();
        return {
            elem: {
                style: style,
                width: rectElem.width,
                height: rectElem.height,
                top: rectElem.top,
                bottom: rectElem.bottom,
                left: rectElem.left,
                right: rectElem.right,
                margin: getBoxStyle(elem, 'margin', style),
                border: getBoxStyle(elem, 'border', style)
            },
            parent: {
                style: parentStyle,
                width: rectParent.width,
                height: rectParent.height,
                top: rectParent.top,
                bottom: rectParent.bottom,
                left: rectParent.left,
                right: rectParent.right,
                padding: getBoxStyle(parent, 'padding', parentStyle),
                border: getBoxStyle(parent, 'border', parentStyle)
            }
        };
    }

    /**
     * Determine if an element is attached to the DOM
     * Panzoom requires this so events work properly
     */
    function isAttached(elem) {
        var doc = elem.ownerDocument;
        var parent = elem.parentNode;
        return (doc &&
            parent &&
            doc.nodeType === 9 &&
            parent.nodeType === 1 &&
            doc.documentElement.contains(parent));
    }

    function getClass(elem) {
        return (elem.getAttribute('class') || '').trim();
    }
    function hasClass(elem, className) {
        return elem.nodeType === 1 && (" " + getClass(elem) + " ").indexOf(" " + className + " ") > -1;
    }
    function isExcluded(elem, options) {
        for (var cur = elem; cur != null; cur = cur.parentNode) {
            if (hasClass(cur, options.excludeClass) || options.exclude.indexOf(cur) > -1) {
                return true;
            }
        }
        return false;
    }

    /**
     * Determine if an element is SVG by checking the namespace
     * Exception: the <svg> element itself should be treated like HTML
     */
    var rsvg = /^http:[\w\.\/]+svg$/;
    function isSVGElement(elem) {
        return rsvg.test(elem.namespaceURI) && elem.nodeName.toLowerCase() !== 'svg';
    }

    function shallowClone(obj) {
        var clone = {};
        for (var key in obj) {
            if (obj.hasOwnProperty(key)) {
                clone[key] = obj[key];
            }
        }
        return clone;
    }

    var defaultOptions = {
        animate: false,
        canvas: false,
        cursor: 'move',
        disablePan: false,
        disableZoom: false,
        disableXAxis: false,
        disableYAxis: false,
        duration: 200,
        easing: 'ease-in-out',
        exclude: [],
        excludeClass: 'panzoom-exclude',
        handleStartEvent: function (e) {
            e.preventDefault();
            e.stopPropagation();
        },
        maxScale: 4,
        minScale: 0.125,
        overflow: 'hidden',
        panOnlyWhenZoomed: false,
        relative: false,
        setTransform: setTransform,
        startX: 0,
        startY: 0,
        startScale: 1,
        step: 0.3,
        touchAction: 'none'
    };
    function Panzoom(elem, options) {
        if (!elem) {
            throw new Error('Panzoom requires an element as an argument');
        }
        if (elem.nodeType !== 1) {
            throw new Error('Panzoom requires an element with a nodeType of 1');
        }
        if (!isAttached(elem)) {
            throw new Error('Panzoom should be called on elements that have been attached to the DOM');
        }
        options = __assign(__assign({}, defaultOptions), options);
        var isSVG = isSVGElement(elem);
        var parent = elem.parentNode;
        // Set parent styles
        parent.style.overflow = options.overflow;
        parent.style.userSelect = 'none';
        // This is important for mobile to
        // prevent scrolling while panning
        parent.style.touchAction = options.touchAction;
        (options.canvas ? parent : elem).style.cursor = options.cursor;
        // Set element styles
        elem.style.userSelect = 'none';
        elem.style.touchAction = options.touchAction;
        // The default for SVG is '0 0'
        // SVG can't be changed in IE
        setStyle(elem, 'transformOrigin', typeof options.origin === 'string' ? options.origin : isSVG ? '0 0' : '50% 50%');
        function setOptions(opts) {
            if (opts === void 0) { opts = {}; }
            for (var key in opts) {
                if (opts.hasOwnProperty(key)) {
                    options[key] = opts[key];
                }
            }
            // Handle option side-effects
            if (opts.hasOwnProperty('cursor')) {
                elem.style.cursor = opts.cursor;
            }
            if (opts.hasOwnProperty('overflow')) {
                parent.style.overflow = opts.overflow;
            }
            if (opts.hasOwnProperty('touchAction')) {
                parent.style.touchAction = opts.touchAction;
                elem.style.touchAction = opts.touchAction;
            }
            if (opts.hasOwnProperty('minScale') ||
                opts.hasOwnProperty('maxScale') ||
                opts.hasOwnProperty('contain')) {
                setMinMax();
            }
        }
        var x = 0;
        var y = 0;
        var scale = 1;
        var isPanning = false;
        zoom(options.startScale, { animate: false });
        // Wait for scale to update
        // for accurate dimensions
        // to constrain initial values
        setTimeout(function () {
            setMinMax();
            pan(options.startX, options.startY, { animate: false });
        });
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        function trigger(eventName, detail, opts) {
            if (opts.silent) {
                return;
            }
            var event = new CustomEvent(eventName, { detail: detail });
            elem.dispatchEvent(event);
        }
        function setTransformWithEvent(eventName, opts) {
            var value = { x: x, y: y, scale: scale, isSVG: isSVG };
            requestAnimationFrame(function () {
                if (typeof opts.animate === 'boolean') {
                    if (opts.animate) {
                        setTransition(elem, opts);
                    }
                    else {
                        setStyle(elem, 'transition', 'none');
                    }
                }
                opts.setTransform(elem, value, opts);
            });
            trigger(eventName, value, opts);
            trigger('panzoomchange', value, opts);
            return value;
        }
        function setMinMax() {
            if (options.contain) {
                var dims = getDimensions(elem);
                var parentWidth = dims.parent.width - dims.parent.border.left - dims.parent.border.right;
                var parentHeight = dims.parent.height - dims.parent.border.top - dims.parent.border.bottom;
                var elemWidth = dims.elem.width / scale;
                var elemHeight = dims.elem.height / scale;
                var elemScaledWidth = parentWidth / elemWidth;
                var elemScaledHeight = parentHeight / elemHeight;
                if (options.contain === 'inside') {
                    options.maxScale = Math.min(elemScaledWidth, elemScaledHeight);
                }
                else if (options.contain === 'outside') {
                    options.minScale = Math.max(elemScaledWidth, elemScaledHeight);
                }
            }
        }
        function constrainXY(toX, toY, toScale, panOptions) {
            var opts = __assign(__assign({}, options), panOptions);
            var result = { x: x, y: y, opts: opts };
            if (!opts.force && (opts.disablePan || (opts.panOnlyWhenZoomed && scale === opts.startScale))) {
                return result;
            }
            toX = parseFloat(toX);
            toY = parseFloat(toY);
            if (!opts.disableXAxis) {
                result.x = (opts.relative ? x : 0) + toX;
            }
            if (!opts.disableYAxis) {
                result.y = (opts.relative ? y : 0) + toY;
            }
            if (opts.contain === 'inside') {
                var dims = getDimensions(elem);
                result.x = Math.max(-dims.elem.margin.left - dims.parent.padding.left, Math.min(dims.parent.width -
                    dims.elem.width / toScale -
                    dims.parent.padding.left -
                    dims.elem.margin.left -
                    dims.parent.border.left -
                    dims.parent.border.right, result.x));
                result.y = Math.max(-dims.elem.margin.top - dims.parent.padding.top, Math.min(dims.parent.height -
                    dims.elem.height / toScale -
                    dims.parent.padding.top -
                    dims.elem.margin.top -
                    dims.parent.border.top -
                    dims.parent.border.bottom, result.y));
            }
            else if (opts.contain === 'outside') {
                var dims = getDimensions(elem);
                var realWidth = dims.elem.width / scale;
                var realHeight = dims.elem.height / scale;
                var scaledWidth = realWidth * toScale;
                var scaledHeight = realHeight * toScale;
                var diffHorizontal = (scaledWidth - realWidth) / 2;
                var diffVertical = (scaledHeight - realHeight) / 2;
                var minX = (-(scaledWidth - dims.parent.width) -
                    dims.parent.padding.left -
                    dims.parent.border.left -
                    dims.parent.border.right +
                    diffHorizontal) /
                    toScale;
                var maxX = (diffHorizontal - dims.parent.padding.left) / toScale;
                result.x = Math.max(Math.min(result.x, maxX), minX);
                var minY = (-(scaledHeight - dims.parent.height) -
                    dims.parent.padding.top -
                    dims.parent.border.top -
                    dims.parent.border.bottom +
                    diffVertical) /
                    toScale;
                var maxY = (diffVertical - dims.parent.padding.top) / toScale;
                result.y = Math.max(Math.min(result.y, maxY), minY);
            }
            return result;
        }
        function constrainScale(toScale, zoomOptions) {
            var opts = __assign(__assign({}, options), zoomOptions);
            var result = { scale: scale, opts: opts };
            if (!opts.force && opts.disableZoom) {
                return result;
            }
            result.scale = Math.min(Math.max(toScale, opts.minScale), opts.maxScale);
            return result;
        }
        function pan(toX, toY, panOptions) {
            var result = constrainXY(toX, toY, scale, panOptions);
            var opts = result.opts;
            x = result.x;
            y = result.y;
            return setTransformWithEvent('panzoompan', opts);
        }
        function zoom(toScale, zoomOptions) {
            var result = constrainScale(toScale, zoomOptions);
            var opts = result.opts;
            if (!opts.force && opts.disableZoom) {
                return;
            }
            toScale = result.scale;
            var toX = x;
            var toY = y;
            if (opts.focal) {
                // The difference between the point after the scale and the point before the scale
                // plus the current translation after the scale
                // neutralized to no scale (as the transform scale will apply to the translation)
                var focal = opts.focal;
                toX = (focal.x / toScale - focal.x / scale + x * toScale) / toScale;
                toY = (focal.y / toScale - focal.y / scale + y * toScale) / toScale;
            }
            var panResult = constrainXY(toX, toY, toScale, { relative: false, force: true });
            x = panResult.x;
            y = panResult.y;
            scale = toScale;
            return setTransformWithEvent('panzoomzoom', opts);
        }
        function zoomInOut(isIn, zoomOptions) {
            var opts = __assign(__assign(__assign({}, options), { animate: true }), zoomOptions);
            return zoom(scale * Math.exp((isIn ? 1 : -1) * opts.step), opts);
        }
        function zoomIn(zoomOptions) {
            return zoomInOut(true, zoomOptions);
        }
        function zoomOut(zoomOptions) {
            return zoomInOut(false, zoomOptions);
        }
        function zoomToPoint(toScale, point, zoomOptions) {
            var dims = getDimensions(elem);
            // Instead of thinking of operating on the panzoom element,
            // think of operating on the area inside the panzoom
            // element's parent
            // Subtract padding and border
            var effectiveArea = {
                width: dims.parent.width -
                    dims.parent.padding.left -
                    dims.parent.padding.right -
                    dims.parent.border.left -
                    dims.parent.border.right,
                height: dims.parent.height -
                    dims.parent.padding.top -
                    dims.parent.padding.bottom -
                    dims.parent.border.top -
                    dims.parent.border.bottom
            };
            // Adjust the clientX/clientY to ignore the area
            // outside the effective area
            var clientX = point.clientX -
                dims.parent.left -
                dims.parent.padding.left -
                dims.parent.border.left -
                dims.elem.margin.left;
            var clientY = point.clientY -
                dims.parent.top -
                dims.parent.padding.top -
                dims.parent.border.top -
                dims.elem.margin.top;
            // Adjust the clientX/clientY for HTML elements,
            // because they have a transform-origin of 50% 50%
            if (!isSVG) {
                clientX -= dims.elem.width / scale / 2;
                clientY -= dims.elem.height / scale / 2;
            }
            // Convert the mouse point from it's position over the
            // effective area before the scale to the position
            // over the effective area after the scale.
            var focal = {
                x: (clientX / effectiveArea.width) * (effectiveArea.width * toScale),
                y: (clientY / effectiveArea.height) * (effectiveArea.height * toScale)
            };
            return zoom(toScale, __assign(__assign({ animate: false }, zoomOptions), { focal: focal }));
        }
        function zoomWithWheel(event, zoomOptions) {
            // Need to prevent the default here
            // or it conflicts with regular page scroll
            event.preventDefault();
            var opts = __assign(__assign({}, options), zoomOptions);
            // Normalize to deltaX in case shift modifier is used on Mac
            var delta = event.deltaY === 0 && event.deltaX ? event.deltaX : event.deltaY;
            var wheel = delta < 0 ? 1 : -1;
            var toScale = constrainScale(scale * Math.exp((wheel * opts.step) / 3), opts).scale;
            return zoomToPoint(toScale, event, opts);
        }
        function reset(resetOptions) {
            var opts = __assign(__assign(__assign({}, options), { animate: true, force: true }), resetOptions);
            scale = constrainScale(opts.startScale, opts).scale;
            var panResult = constrainXY(opts.startX, opts.startY, scale, opts);
            x = panResult.x;
            y = panResult.y;
            return setTransformWithEvent('panzoomreset', opts);
        }
        var origX;
        var origY;
        var startClientX;
        var startClientY;
        var startScale;
        var startDistance;
        var pointers = [];
        function handleDown(event) {
            // Don't handle this event if the target is excluded
            if (isExcluded(event.target, options)) {
                return;
            }
            addPointer(pointers, event);
            isPanning = true;
            options.handleStartEvent(event);
            origX = x;
            origY = y;
            trigger('panzoomstart', { x: x, y: y, scale: scale }, options);
            // This works whether there are multiple
            // pointers or not
            var point = getMiddle(pointers);
            startClientX = point.clientX;
            startClientY = point.clientY;
            startScale = scale;
            startDistance = getDistance(pointers);
        }
        function move(event) {
            if (!isPanning ||
                origX === undefined ||
                origY === undefined ||
                startClientX === undefined ||
                startClientY === undefined) {
                return;
            }
            addPointer(pointers, event);
            var current = getMiddle(pointers);
            if (pointers.length > 1) {
                // Use the distance between the first 2 pointers
                // to determine the current scale
                var diff = getDistance(pointers) - startDistance;
                var toScale = constrainScale((diff * options.step) / 80 + startScale).scale;
                zoomToPoint(toScale, current);
            }
            pan(origX + (current.clientX - startClientX) / scale, origY + (current.clientY - startClientY) / scale, {
                animate: false
            });
        }
        function handleUp(event) {
            // Don't call panzoomend when panning with 2 touches
            // until both touches end
            if (pointers.length === 1) {
                trigger('panzoomend', { x: x, y: y, scale: scale }, options);
            }
            // Note: don't remove all pointers
            // Can restart without having to reinitiate all of them
            // Remove the pointer regardless of the isPanning state
            removePointer(pointers, event);
            if (!isPanning) {
                return;
            }
            isPanning = false;
            origX = origY = startClientX = startClientY = undefined;
        }
        var bound = false;
        function bind() {
            if (bound) {
                return;
            }
            bound = true;
            onPointer('down', options.canvas ? parent : elem, handleDown);
            onPointer('move', document, move, { passive: true });
            onPointer('up', document, handleUp, { passive: true });
        }
        function destroy() {
            bound = false;
            destroyPointer('down', options.canvas ? parent : elem, handleDown);
            destroyPointer('move', document, move);
            destroyPointer('up', document, handleUp);
        }
        if (!options.noBind) {
            bind();
        }
        return {
            bind: bind,
            destroy: destroy,
            eventNames: events,
            getPan: function () { return ({ x: x, y: y }); },
            getScale: function () { return scale; },
            getOptions: function () { return shallowClone(options); },
            pan: pan,
            reset: reset,
            setOptions: setOptions,
            setStyle: function (name, value) { return setStyle(elem, name, value); },
            zoom: zoom,
            zoomIn: zoomIn,
            zoomOut: zoomOut,
            zoomToPoint: zoomToPoint,
            zoomWithWheel: zoomWithWheel
        };
    }
    Panzoom.defaultOptions = defaultOptions;

    return Panzoom;

})));
