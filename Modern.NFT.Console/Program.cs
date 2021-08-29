using System;
using Modern.NFT.Generator;
using Modern.NFT.Generator.Avatar;

namespace Modern.NFT.Console
{
    class Program : BaseAvatar
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Started generating the NFT Art work");

            IModernArtGenerator modernArtGenerator = new ModernArtGenerator(500, 500);
            modernArtGenerator.Generate(Guid.NewGuid().ToString(), 5,
                GeometricStyle.Geometric2, null,
                withGlass: true,
                withMask: false,
                withSmoke: true,
                withHat: false);

            ArtBlockGenerator artBlockGenerator = new ArtBlockGenerator();
            artBlockGenerator.GenerateDesign1(10);

            GridyGenerator gridyGenerator = new GridyGenerator();
            gridyGenerator.Generate(100, 500, 500, 24);

            IdenticonGenerator identIconGenerator = new IdenticonGenerator();
            identIconGenerator.Generate(50);

            FemaleAvatarTriat avatarTriat = new FemaleAvatarTriat
            {
                WithShirt = false,
                WithTop = false,

                WithPant = false,
                WithBlouse = false,
                WithScarf = false,

                WithHolster = true,

                WithBracelet = true,
                WithNecklace = true,
                WithLeggings = true,
                WithMakeup = true,
                WithArmband = true,
                WithDress = true,
                WithCoat = true,
                WithCollar = true,
                WithHairs = true,
                WithSmoke = true,
                WithHat = true,
                WithMask = true,
            };

            FemaleAvatarGenerator femaleAvatarGenerator = new FemaleAvatarGenerator();
            femaleAvatarGenerator.Generate(2000, 2000,
                2, avatarTriat);

            IndoBitGeneratorStandard indoBitGenerator = new IndoBitGeneratorStandard();
            indoBitGenerator.GenerateBots();

            VoxalManGenerator voxalManGenerator = new VoxalManGenerator();
            voxalManGenerator.GenerateVoxalMan(30);

            SuperGirlGenerator superGirlGenerator = new SuperGirlGenerator();
            superGirlGenerator.GenerateSuperGirl(10);

            BotGenerator botGenerator = new BotGenerator();
            botGenerator.GenerateBots(50);

            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadLine();
        }
    }
}
