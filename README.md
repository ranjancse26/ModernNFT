# ModernNFT
The Modern NFT is a "Digital Art" Generator Project. Uses custom generative art algorithms for generating the NFT Artwork.

The following are the custom generators built as part of the library. You may extend the library to have more :)

1) ArtBlockGenerator
2) BotGenerator
3) FemaleAvatar Generator
4) GridyGenerator
5) IdenticonGenerator
6) IndoBitGeneratorCustom
7) IndoBitGeneratorStandard
8) ModernArtGenerator
9) SuperGirlGenerator
10) VoxalManGenerator

# Goal

The goal or purpose of this project is to build a highly scalable NFT Generative Art using custom generative algorithms.

# Overview

The Modern.NFT project is a collection of scalable NFT Artwork Project created using C# .NET Core.

# Tools & Technologies

The following are the tools are technologies used

1) VS 2019 Community Edition
2) Inkspace
3) GIMP

# Behind the scene

The Modern Art for example is generated with the help of a base SVG image created using the Inkspace an open-source vector graphics tool. The program makes use of the below subset of SVG images. Each of the traits consists of a series of artwork and the generator is responsible for building the NFT Artwork by randomly picking the traits, applying the necessary background with the genetic color-coding technology.

# How to generate the Modern NFT Art?

You will be seeing how to generate Modern NFT Artwork based on the concept of Generators.
The following code snippet generates the Modern Art with a count of 50 for example. As you can see below, a Geometric background is being applied with the traits of your interest.

```
 IModernArtGenerator modernArtGenerator = new ModernArtGenerator(500, 500);
            modernArtGenerator.Generate(Guid.NewGuid().ToString(), 50,
                GeometricStyle.Geometric2, null,
                withGlass: true,
                withMask: false,
                withSmoke: true,
                withHat: false);
```

# How to Run?

Set as a Startup Project on "Modern.NFT.Console" Project. It's a console application consisting of a "Program.cs" that has all the logic for generating the Modern NFT Art. Control F5 or Debug the project to know more.

# Inspiration

This project is inspired by various other open source work. 

https://avatars.dicebear.com/
https://www.flaticon.com/
https://github.com/goabstract/Awesome-Design-Tools
https://github.com/ubik23/charactercreator

# Commercial Usage

This source code is copyrighted to Ranjan Dailata. 

Please note that the code is open source. You are free to use it. However, you need to get an explicit license to use for commercial purpose. You may take an owernship or rights to publish to NFT Platform with an agreed upon terms and conditions.