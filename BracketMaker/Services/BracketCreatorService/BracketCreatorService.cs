using BracketMaker.Controllers;
using BracketMaker.Context;

namespace BracketMaker.Services.BracketCreatorService
{
    public class BracketCreatorService
    {
        private readonly Context.ItemContext _itemContext;

        public BracketCreatorService(Context.ItemContext itemContext) =>
            _itemContext = itemContext;
    
    }
}
