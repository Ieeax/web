using System;
using System.Text;

namespace Leeax.Web.Components.Cookies
{
    public class CookieOptions
    {
        public CookieOptions()
        {
        }

        public CookieOptions(string? name, string? value)
            : this(name, value, TimeSpan.MinValue)
        {
        }

        public CookieOptions(string? name, string? value, TimeSpan maxAge)
        {
            Name = name;
            Value = value;
            MaxAge = maxAge;
        }

        /// <summary>
        /// Checks the <see cref="CookieOptions"/> and returns them as a javascript cookie string.
        /// If this method returns <see langword="false"/> the returned <see cref="value"/> will be invalid.
        /// </summary>
        /// <param name="value">The cookie string. Will never be null.</param>
        public bool TryGetCookieString(out string value)
        {
            var result = !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Value);
            var builder = new StringBuilder();

            builder.Append(Name + "=");

            if (Value != null)
            {
                builder.Append(Uri.EscapeDataString(Value));
            }

            if (Path != null)
            {
                builder.Append(";path=" + Path);
            }

            if (Domain != null)
            {
                builder.Append(";domain=" + Domain);
            }

            if (MaxAge > TimeSpan.MinValue)
            {
                builder.Append(";max-age=" + (long)MaxAge.TotalSeconds);
            }

            if (Secure)
            {
                builder.Append(";secure");
            }

            var sameSiteMode = MapSameSiteMode(SameSite);
            if (sameSiteMode != null)
            {
                builder.Append(";samesite=" + sameSiteMode);
            }

            value = builder.ToString();

            return result;
        }

        /// <summary>
        /// Gets the <see cref="CookieOptions"/> as a javascript cookie string.
        /// <para>If required values like <see cref="Name"/> or <see cref="Value"/> are not set, the string will be invalid.</para>
        /// </summary>
        public override string ToString()
        {
            TryGetCookieString(out var value);
            return value;
        }

        private static string? MapSameSiteMode(SameSiteMode value)
        {
            return value switch
            {
                SameSiteMode.None => "none",
                SameSiteMode.Lax => "lax",
                SameSiteMode.Strict => "strict",
                _ => null
            };
        }

        /// <summary>
        /// Gets or sets the cookie name/key.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the cookie value.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets the cookie path.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the domain to associate the cookie with.
        /// </summary>
        public string? Domain { get; set; }

        /// <summary>
        /// Gets or sets the max-age for the cookie.
        /// </summary>
        public TimeSpan MaxAge { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to transmit the cookie using Secure Sockets Layer (SSL) that is, over HTTPS only.
        /// </summary>
        public bool Secure { get; set; }

        /// <summary>
        /// Gets or sets the value for the SameSite attribute of the cookie. The default value is <see cref="SameSiteMode.Unspecified"/>.
        /// </summary>
        public SameSiteMode SameSite { get; set; }

        public static CookieOptions Default => new CookieOptions();
    }
}