using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    /// <summary>
    /// Tools data-structure for identifying the tools within the system in the form of enums.
    /// </summary>
    public static class Tools
    {
        public enum Categories
        {
            GardeningTools,
            FlooringTools,
            FencingTools,
            MeasuringTools,
            CleaningTools,
            PaintingTools,
            ElectronicTools,
            ElectricityTools,
            AutomotiveTools,
        }

        /// <summary>
        /// Returns Tool Sub-Category enum as type from category index.
        /// </summary>
        /// <param name="index">Integer index of from the <see cref="Tools.Categories"/> enum.</param>
        /// <returns>Returns Tool Sub-Category enum as type from category index.</returns>
        public static Type SubCategory(int index)
        {
            return index switch
            {
                0 => typeof(GardeningTools),
                1 => typeof(FlooringTools),
                2 => typeof(FencingTools),
                3 => typeof(MeasuringTools),
                4 => typeof(CleaningTools),
                5 => typeof(PaintingTools),
                6 => typeof(ElectronicTools),
                7 => typeof(ElectricityTools),
                8 => typeof(AutomotiveTools),
                _ => null,
            };

        }

        public enum GardeningTools
        {
            LineTrimmers,
            LawnMowers,
            HandTools,
            Wheelbarrows,
            GardenPowerTools
        }
        public enum FlooringTools
        {
            Scrapers,
            FloorLasers,
            FloorLevellingTools,
            FloorLevellingMaterials,
            FloorHandTools,
            TilingTools
        }
        public enum FencingTools
        {
            HandTools,
            ElectricFencing,
            SteelFencingTools,
            PowerTools,
            FencingAccessories
        }
        public enum MeasuringTools
        {
            DistanceTools,
            LaserMeasurer,
            MeasuringJugs,
            TemperatureAndHumidityTools,
            LevellingTools,
            Markers
        }
        public enum CleaningTools
        {
            Draining,
            CarCleaning,
            Vacuum,
            PressureCleaners,
            PoolCleaning,
            FloorCleaning
        }
        public enum PaintingTools
        {
            SandingTools,
            Brushes,
            Rollers,
            PaintRemovalTools,
            PaintScrapers,
            Sprayers
        }
        public enum ElectronicTools
        {
            VoltageTester,
            Oscilloscopes,
            ThermalImaging,
            DataTestTool,
            InsulationTesters
        }
        public enum ElectricityTools
        {
            TestEquipment,
            SafetyEquipment,
            BasicHandTools,
            CircuitProtection,
            CableTools
        }
        public enum AutomotiveTools
        {
            Jacks,
            AirCompressors,
            BatteryChargers,
            SocketTools,
            Braking,
            DriveTrain
        }
    }
}
