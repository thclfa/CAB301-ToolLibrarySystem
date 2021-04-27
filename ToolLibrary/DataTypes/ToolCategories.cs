using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CAB301_ToolLibrarySystem
{
    public static class ToolTypes
    {
        // https://stackoverflow.com/questions/980766/how-do-i-declare-a-nested-enum

        public static readonly ToolType GardeningTool = 1;
        public static class GardeningTools
        {
            public static readonly ToolType LineTrimmer = GardeningTool[0];
            public static readonly ToolType LawnMower = GardeningTool[1];
            public static readonly ToolType HandTool = GardeningTool[2];
            public static readonly ToolType WheelBarrow = GardeningTool[3];
            public static readonly ToolType GardenPowerTool = GardeningTool[4];
        }

        public static readonly ToolType FlooringTool = 2;
        public static class FlooringTools
        {
            public static readonly ToolType Scraper = FlooringTool[0];
            public static readonly ToolType FloorLaser = FlooringTool[1];
            public static readonly ToolType FloorLevellingTool = FlooringTool[2];
            public static readonly ToolType FloorLevellingMaterial = FlooringTool[3];
            public static readonly ToolType FloorHandTool = FlooringTool[4];
            public static readonly ToolType TilingTool = FlooringTool[5];
        }

        public static readonly ToolType FencingTool = 3;
        public static class FencingTools
        {
            public static readonly ToolType HandTool = FencingTool[0];
            public static readonly ToolType ElectricFencing = FencingTool[1];
            public static readonly ToolType SteelFencingTool = FencingTool[2];
            public static readonly ToolType PowerTool = FencingTool[3];
            public static readonly ToolType FencingAccessory = FencingTool[4];
        }

        public static readonly ToolType MeasuringTool = 4;
        public static class MeasuringTools
        {
            public static readonly ToolType DistanceTool = MeasuringTool[0];
            public static readonly ToolType LaserMeasurer = MeasuringTool[1];
            public static readonly ToolType MeasuringJug = MeasuringTool[2];
            public static readonly ToolType TemperatureAndHumidityTool = MeasuringTool[3];
            public static readonly ToolType LevellingTool = MeasuringTool[4];
            public static readonly ToolType Marker = MeasuringTool[5];
        }

        public static readonly ToolType CleaningTool = 5;
        public static class CleaningTools
        {
            public static readonly ToolType Draining = CleaningTool[0];
            public static readonly ToolType CarCleaning = CleaningTool[1];
            public static readonly ToolType Vacuum = CleaningTool[2];
            public static readonly ToolType PressureCleaner = CleaningTool[3];
            public static readonly ToolType PoolCleaning = CleaningTool[4];
            public static readonly ToolType FloorCleaning = CleaningTool[5];
        }

        public static readonly ToolType PaintingTool = 6;
        public static class PaintingTools
        {
            public static readonly ToolType SandingTool = PaintingTool[0];
            public static readonly ToolType Brush = PaintingTool[1];
            public static readonly ToolType Roller = PaintingTool[2];
            public static readonly ToolType PaintremovalTool = PaintingTool[3];
            public static readonly ToolType PaintScraper = PaintingTool[4];
            public static readonly ToolType Sprayer = PaintingTool[5];
        }

        public static readonly ToolType ElectronicTool = 7;
        public static class ElectronicTools
        {
            public static readonly ToolType VoltageTester = ElectronicTool[0];
            public static readonly ToolType Oscilloscope = ElectronicTool[1];
            public static readonly ToolType ThermalImaging = ElectronicTool[2];
            public static readonly ToolType DataTestTool = ElectronicTool[3];
            public static readonly ToolType InsulationTester = ElectronicTool[4];
        }

        public static readonly ToolType ElectricityTool = 8;
        public static class ElectricityTools
        {
            public static readonly ToolType TestEquipment = ElectricityTool[0];
            public static readonly ToolType SafetyEquipment = ElectricityTool[1];
            public static readonly ToolType BasicHandTool = ElectricityTool[2];
            public static readonly ToolType CircuitProtection = ElectricityTool[3];
            public static readonly ToolType CableTool = ElectricityTool[4];
        }

        public static readonly ToolType AutomotiveTool = 9;
        public static class AutomotiveTools
        {
            public static readonly ToolType Jack = AutomotiveTool[0];
            public static readonly ToolType AirCompressor = AutomotiveTool[1];
            public static readonly ToolType BatteryCharger = AutomotiveTool[2];
            public static readonly ToolType SocketTool = AutomotiveTool[3];
            public static readonly ToolType Braking = AutomotiveTool[4];
            public static readonly ToolType Drivetrain = AutomotiveTool[5];
        }

        public static ToolType FromString(string category, string type)
        {
            Type Category = typeof(ToolTypes).GetNestedType(category, BindingFlags.Public | BindingFlags.Static);
            FieldInfo Field = Category.GetField(type, BindingFlags.Public | BindingFlags.Static);

            return (ToolType)Field.GetValue(null);
        }
    }

    public struct ToolType
    {
        public static ToolType none;
        private readonly uint ID;

        public ToolType this[uint id]
        {
            get { return new ToolType((ID << 8) | id); }
        }

        public ToolType Type => new ToolType(ID >> 8);

        public bool IsType(ToolType type)
        {
            return (this != none) && ((Type == type) || Type.IsType(type));
        }

        public static implicit operator ToolType(int id)
        {
            return new ToolType((uint)id);
        }
        public static bool operator ==(ToolType a, ToolType b) => a.ID == b.ID;
        public static bool operator !=(ToolType a, ToolType b) => a.ID != b.ID;
        public override bool Equals(object obj)
        {
            if (obj is ToolType type)
                return type.ID == ID;
            else
                return false;
        }
        public override int GetHashCode() => (int)ID;

        public ToolType(uint id)
        {
            ID = id;
        }
    }
}
