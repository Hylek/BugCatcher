using DC.MessageService;
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace EventMessages
{
    public class OpenMainMenuMessage : ITinyMessage { public object Sender { get; } }
}