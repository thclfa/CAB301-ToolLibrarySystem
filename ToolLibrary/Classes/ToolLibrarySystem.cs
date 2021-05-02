using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    class ToolLibrarySystem : iToolLibrarySystem
    {
        public ToolCollection[][] Tools = new ToolCollection[9][]
        {
            new ToolCollection[5]   
            {
                new ToolCollection(),   // LineTrimmers    
                new ToolCollection(),   // LawnMowers      
                new ToolCollection(),   // HandTools       
                new ToolCollection(),   // Wheelbarrows    
                new ToolCollection()    // GardenPowerTools
            },  // 0. GardeningTools
            new ToolCollection[6]   
            {
                new ToolCollection(),   // Scrapers
                new ToolCollection(),   // FloorLasers
                new ToolCollection(),   // FloorLevellingTools
                new ToolCollection(),   // FloorLevellingMaterials
                new ToolCollection(),   // FloorHandTools
                new ToolCollection()    // TilingTools
            },  // 1. FlooringTools
            new ToolCollection[5]   
            {
                new ToolCollection(),   // HandTools
                new ToolCollection(),   // ElectricFencing
                new ToolCollection(),   // SteelFencingTools
                new ToolCollection(),   // PowerTools
                new ToolCollection()    // FencingAccessories
            },  // 2. FencingTools
            new ToolCollection[6]   
            {
                new ToolCollection(),   // DistanceTools
                new ToolCollection(),   // LaserMeasurer
                new ToolCollection(),   // MeasuringJugs
                new ToolCollection(),   // TemperatureAndHumidityTools
                new ToolCollection(),   // LevellingTools
                new ToolCollection()    // Markers
            },  // 3. MeasuringTools
            new ToolCollection[6]   
            {
                new ToolCollection(),   // Draining
                new ToolCollection(),   // CarCleaning
                new ToolCollection(),   // Vacuum
                new ToolCollection(),   // PressureCleaners
                new ToolCollection(),   // PoolCleaning
                new ToolCollection()    // FloorCleaning
            },  // 4. CleaningTools
            new ToolCollection[6]   
            {
                new ToolCollection(),   // SandingTools
                new ToolCollection(),   // Brushes
                new ToolCollection(),   // Rollers
                new ToolCollection(),   // PaintRemovalTools
                new ToolCollection(),   // PaintScrapers
                new ToolCollection()    // Sprayers
            },  // 5. PaintingTools
            new ToolCollection[5]   
            {
                new ToolCollection(),   // VoltageTester
                new ToolCollection(),   // Oscilloscopes
                new ToolCollection(),   // ThermalImaging
                new ToolCollection(),   // DataTestTool
                new ToolCollection()    // InsulationTesters
            },  // 6. ElectronicTools
            new ToolCollection[5]   
            {
                new ToolCollection(),   // TestEquipment
                new ToolCollection(),   // SafetyEquipment
                new ToolCollection(),   // BasicHandTools
                new ToolCollection(),   // CircuitProtection
                new ToolCollection()    // CableTools
            },  // 7. ElectricityTools
            new ToolCollection[6]   
            {
                new ToolCollection(),   // Jacks
                new ToolCollection(),   // AirCompressors
                new ToolCollection(),   // BatteryChargers
                new ToolCollection(),   // SocketTools
                new ToolCollection(),   // Braking
                new ToolCollection()    // DriveTrain
            }   // 8. AutomotiveTools
        };

        public MemberCollection Members = new MemberCollection();

        public void add(Tool aTool)
        {
            string[] split = aTool.Name.Split(StringLib.DELIMITER);
            int collection = split[0][0];
            int subType = split[1][0];

            Tools[collection][subType].add(aTool);
        }

        public void add(Tool aTool, int quantity)
        {
            string[] split = aTool.Name.Split(StringLib.DELIMITER);
            int collection = split[0][0];
            int subType = split[1][0];

            Tools[collection][subType].add(aTool);
        }

        public void add(Member aMember)
        {
            Members.add(aMember);
        }

        public void borrowTool(Member aMember, Tool aTool)
        {
            throw new NotImplementedException();
        }

        public void delete(Tool aTool)
        {
            throw new NotImplementedException();
        }

        public void delete(Tool aTool, int quantity)
        {
            throw new NotImplementedException();
        }

        public void delete(Member aMember)
        {
            Members.delete(aMember);
        }

        public void displayBorrowingTools(Member aMember)
        {
            throw new NotImplementedException();
        }

        public void displayTools(string aToolType)
        {
            throw new NotImplementedException();
        }

        public void displayTopThree()
        {
            Tool[] sorted = Tools[0][0].toArray();
            sorted.OrderBy(x => x.NoBorrowings);
            
            for (int i = 0; i < 3; i++)
            {
                Tool tool = sorted[i];
                Console.WriteLine("{0}. {1} has been borrowed {2} times.", i, tool.Name, tool.NoBorrowings);
            }
        }

        public string[] listTools(Member aMember)
        {
            throw new NotImplementedException();
        }

        public void returnTool(Member aMember, Tool aTool)
        {
            throw new NotImplementedException();
        }
    }
}
