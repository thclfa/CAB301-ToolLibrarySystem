using System;
using System.Collections.Generic;
using System.Text;

//namespace CAB301_ToolLibrarySystem
//{
//    public struct zToolType
//    {
//        private readonly uint Index;
//        public ToolType Category => new ToolType(this.Index >> 8);

//        private ToolType(uint index)
//        {
//            this.Index = index;
//        }

//        public ToolType this[int index]
//        {
//            get { return new ToolType((this.Index << 8) | (uint)index); }
//        }

//        public bool IsType(ToolType category)
//        {
//            return ((Category == category) || Category.IsType(category));
//        }

//        public static implicit operator ToolType(int index) => new ToolType((uint)index);
//        public static bool operator ==(ToolType a, ToolType b) => a.Index == b.Index;
//        public static bool operator !=(ToolType a, ToolType b) => a.Index != b.Index;
//        public override int GetHashCode() => (int)this.Index;
//        public override bool Equals(object obj) => obj is ToolType && ((ToolType)obj).Index == Index;
//    }

//    public static class zToolTypes
//    {
//        public static readonly ToolType GardeningTool = 0;
//        public static readonly ToolType FlooringTool = 1;
//        public static readonly ToolType FencingTool = 2;
//        public static readonly ToolType MeasuringTool = 3;
//        public static readonly ToolType CleaningTool = 4;
//        public static readonly ToolType PaintingTool = 5;
//        public static readonly ToolType ElectronicTool = 6;
//        public static readonly ToolType ElectricityTool = 7;
//        public static readonly ToolType AutomotiveTool = 8;

//        public static class GardeningTools
//        {
//            public static readonly ToolType LineTrimmers = GardeningTool[0];
//            public static readonly ToolType LawnMowers = GardeningTool[1];
//            public static readonly ToolType HandTools = GardeningTool[2];
//            public static readonly ToolType Wheelbarrows = GardeningTool[3];
//            public static readonly ToolType GardenPowerTools = GardeningTool[4];
//        }
//        public static class FlooringTools
//        {
//            public static readonly ToolType Scrapers = FlooringTool[0];
//            public static readonly ToolType FloorLasers = FlooringTool[1];
//            public static readonly ToolType FloorLevellingTools = FlooringTool[2];
//            public static readonly ToolType FloorLevellingMaterials = FlooringTool[3];
//            public static readonly ToolType FloorHandTools = FlooringTool[4];
//            public static readonly ToolType TilingTools = FlooringTool[5];
//        }
//        public static class FencingTools
//        {
//            public static readonly ToolType HandTools = FencingTool[0];
//            public static readonly ToolType ElectricFencing = FencingTool[1];
//            public static readonly ToolType SteelFencingTools = FencingTool[2];
//            public static readonly ToolType PowerTools = FencingTool[3];
//            public static readonly ToolType FencingAccessories = FencingTool[4];
//        }
//        public static class MeasuringTools
//        {
//            public static readonly ToolType DistanceTools = MeasuringTool[0];
//            public static readonly ToolType LaserMeasurer = MeasuringTool[1];
//            public static readonly ToolType MeasuringJugs = MeasuringTool[2];
//            public static readonly ToolType TemperatureAndHumidityTools = MeasuringTool[3];
//            public static readonly ToolType LevellingTools = MeasuringTool[4];
//            public static readonly ToolType Markers = MeasuringTool[5];
//        }
//        public static class CleaningTools
//        {
//            public static readonly ToolType Draining = CleaningTool[0];
//            public static readonly ToolType CarCleaning = CleaningTool[1];
//            public static readonly ToolType Vacuum = CleaningTool[2];
//            public static readonly ToolType PressureCleaners = CleaningTool[3];
//            public static readonly ToolType PoolCleaning = CleaningTool[4];
//            public static readonly ToolType FloorCleaning = CleaningTool[5];
//        }
//        public static class PaintingTools
//        {                          
//            public static readonly ToolType SandingTools = PaintingTool[0];
//            public static readonly ToolType Brushes = PaintingTool[1];
//            public static readonly ToolType Rollers = PaintingTool[2];
//            public static readonly ToolType PaintRemovalTools = PaintingTool[3];
//            public static readonly ToolType PaintScrapers = PaintingTool[4];
//            public static readonly ToolType Sprayers = PaintingTool[5];
//        }                          
//        public static class ElectronicTools
//        {                          
//            public static readonly ToolType VoltageTester = ElectronicTool[0];
//            public static readonly ToolType Oscilloscopes = ElectronicTool[1];
//            public static readonly ToolType ThermalImaging = ElectronicTool[2];
//            public static readonly ToolType DataTestTool = ElectronicTool[3];
//            public static readonly ToolType InsulationTesters = ElectronicTool[4];
//        }                          
//        public static class ElectricityTools
//        {                          
//            public static readonly ToolType TestEquipment = ElectricityTool[0];
//            public static readonly ToolType SafetyEquipment = ElectricityTool[1];
//            public static readonly ToolType BasicHandTools = ElectricityTool[2];
//            public static readonly ToolType CircuitProtection = ElectricityTool[3];
//            public static readonly ToolType CableTools = ElectricityTool[4];
//        }                          
//        public static class AutomotiveTools
//        {
//            public static readonly ToolType Jacks = AutomotiveTool[0];
//            public static readonly ToolType AirCompressors = AutomotiveTool[1];
//            public static readonly ToolType BatteryChargers = AutomotiveTool[2];
//            public static readonly ToolType SocketTools = AutomotiveTool[3];
//            public static readonly ToolType Braking = AutomotiveTool[4];
//            public static readonly ToolType DriveTrain = AutomotiveTool[5];
//        }
//    }
//}
