using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Shell;

namespace Notisblokk.Model
{
    class Flipper
    {
        public static FlipTileData GetFlipTile(Note n)
        {
            FlipTileData tile = new FlipTileData()
            {
                Title = "My note",
                BackTitle = n.Description,
                BackContent = n.Content,
                WideBackContent = n.Content,
                Count = 1,
                SmallBackgroundImage = new Uri("/Resources/app_icon_small.png", UriKind.Relative),
                BackgroundImage = new Uri("/Resources/app_icon_med.png", UriKind.Relative),
                BackBackgroundImage = new Uri("/Resources/app_icon_med.png", UriKind.Relative),
            };

            return tile;
        }

        public static FlipTileData ClearTileData()
        {
            FlipTileData tile = new FlipTileData()
            {
                Title = "",
                BackTitle = "",
                BackContent = "",
                WideBackContent = "",
                Count = 0,
            };

            return tile;
        }
    }
}
