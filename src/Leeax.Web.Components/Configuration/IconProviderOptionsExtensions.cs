namespace Leeax.Web.Components.Configuration
{
    public static class IconProviderOptionsExtensions
    {
        public static void AddDefaultIcons(this IconProviderOptions options)
        {
            options.Add("today", color => $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 48 48\"><path fill=\"{color}\" d=\"M38 6h-2V2h-4v4H16V2h-4v4h-2c-2.21 0-3.98 1.79-3.98 4L6 38c0 2.21 1.79 4 4 4h28c2.21 0 4-1.79 4-4V10c0-2.21-1.79-4-4-4zm0 32H10V16h28v22zM14 20h10v10H14z\"/></svg>");
            options.Add("schedule", color => $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 48 48\"><path fill=\"{color}\" d=\"M23.99 4C12.94 4 4 12.95 4 24s8.94 20 19.99 20C35.04 44 44 35.05 44 24S35.04 4 23.99 4zM24 40c-8.84 0-16-7.16-16-16S15.16 8 24 8s16 7.16 16 16-7.16 16-16 16zm1-26h-3v12l10.49 6.3L34 29.84l-9-5.34z\"/></svg>");
            options.Add("arrow_upward", color => $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 48 48\"><path fill=\"{color}\" d=\"M8 24l2.83 2.83L22 15.66V40h4V15.66l11.17 11.17L40 24 24 8 8 24z\"/></svg>");
            options.Add("arrow_downward", color => $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 48 48\"><path fill=\"{color}\" d=\"M40 24l-2.82-2.82L26 32.34V8h-4v24.34L10.84 21.16 8 24l16 16 16-16z\"/></svg>");
            options.Add("expand_more", color => $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 48 48\"><path fill=\"{color}\" d=\"M33.17 17.17L24 26.34l-9.17-9.17L12 20l12 12 12-12z\"/></svg>");
            options.Add("keyboard_arrow_right", color => $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 48 48\"><path fill=\"{color}\" d=\"M17.17 32.92l9.17-9.17-9.17-9.17L20 11.75l12 12-12 12z\"/></svg>");
            options.Add("close", color => $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 48 48\"><path fill=\"{color}\" d=\"M38 12.83L35.17 10 24 21.17 12.83 10 10 12.83 21.17 24 10 35.17 12.83 38 24 26.83 35.17 38 38 35.17 26.83 24z\"/></svg>");
            options.Add("refresh", color => $"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 48 48\"><path fill=\"{color}\" d=\"M35.3 12.7C32.41 9.8 28.42 8 24 8 15.16 8 8.02 15.16 8.02 24S15.16 40 24 40c7.45 0 13.69-5.1 15.46-12H35.3c-1.65 4.66-6.07 8-11.3 8-6.63 0-12-5.37-12-12s5.37-12 12-12c3.31 0 6.28 1.38 8.45 3.55L26 22h14V8l-4.7 4.7z\"/></svg>");
        }
    }
}