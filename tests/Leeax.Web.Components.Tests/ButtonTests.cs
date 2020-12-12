using Leeax.Web.Components.Configuration;
using Leeax.Web.Components.Input;
using Leeax.Web.Components.Scopes;
using Xunit;
using Bunit;
using Leeax.Web.Components.Theme;

namespace Leeax.Web.Components.Tests
{
    public class ButtonTests : TestContext
    {
        [Fact]
        public void HasText()
        {
            Services.AddComponents();
            Services.AddModals();
            Services.AddTheming(null);

            RenderTree.Add<LxAppScope>();

            // Add button with text
            var component = RenderComponent<LxButton>(
                builder => builder.Add(x => x.Text, "TEXT"));

            // Ensure that no icon is displayed
            Assert.Null(component.Find(".lx-icon"));

            // Ensure right text
            var spanNode = component.Find("span");
            Assert.Equal("TEXT", spanNode.TextContent);
        }

        [Fact]
        public void HasIcon()
        {
            Services.AddComponents();
            Services.AddModals();
            Services.AddTheming(null);

            RenderTree.Add<LxAppScope>();

            // Add button with icon
            var component = RenderComponent<LxButton>(
                builder => builder.Add(
                    x => x.ChildContent, 
                    x =>
                    {
                        x.OpenComponent<LxButtonIcon>(0);
                        x.CloseComponent();
                    }));

            // Ensure that a icon is displayed
            Assert.NotNull(component.Find(".lx-icon"));

            // Ensure no text
            Assert.Null(component.Find("span"));
        }
    }
}