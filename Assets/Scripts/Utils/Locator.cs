using Core;
using DC.MessageService;

namespace Utils
{
    /// <summary>
    /// Specific implementation of Locator that allows us to neatly declare services here which makes the code that
    /// calls them much cleaner.
    /// </summary>
    public class Locator : BaseLocator
    {
        public static ITinyMessengerHub EventHub
        {
            get
            {
                var hub = Find<ITinyMessengerHub>();

                if (hub != null) return hub;

                var instance = Add<ITinyMessengerHub>(new TinyMessengerHub());

                return instance;
            }
        }

        public static IBugCatcherApp App
        {
            get
            {
                var app = Find<IBugCatcherApp>();

                if (app != null) return app;

                var instance = Add<IBugCatcherApp>(new BugCatcherApp());

                return instance;
            }
        }
    }
}