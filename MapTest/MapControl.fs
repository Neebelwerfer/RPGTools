namespace MapTest

module MapControl = 
    open Inputs
    open Microsoft.Xna.Framework.Graphics

    type MapControl(_graphicsDevice) =
        
        let TileSelected = new Event<MouseEvent>()
        let TileDeselected = new Event<_>()

        member val _GraphicsDevice:GraphicsDevice = _graphicsDevice with get
        member this.TileSelectedEvent = TileSelected.Publish
        member this.TileDeselectedEvent = TileDeselected.Publish
       
