namespace GraphicsManagement
{
    public class GraphicsTypeConverter
    {
        public GraphicsType FromDevice(string device)
        {
            switch (device)
            {
                case "desktop": return GraphicsType.Desktop;

                case "mobile": return GraphicsType.Mobile;

                case "tablet": return GraphicsType.Mobile;

                case "tv": return GraphicsType.Mobile;

                default: return GraphicsType.Desktop;
            }
        }
    }
}
