using KahootBackend.Controllers;
using KahootBackend.Context;

namespace KahootBackend.Services.BracketCreatorService
{
    public class BracketCreatorService
    {
        private readonly Context.ItemContext _itemContext;

        public BracketCreatorService(Context.ItemContext itemContext) =>
            _itemContext = itemContext;
    
    }
}
