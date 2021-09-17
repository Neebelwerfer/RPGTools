namespace MapTest.Camera

module Camera =
    open Microsoft.Xna.Framework.Graphics
    open Microsoft.Xna.Framework

    type Zoom = Zoom of float32
    type Rotation = Rotation of float32
    type Transformation = Matrix
    type Position = Position of Vector2
    type ViewSize = ViewSize of int * int

    type Camera = Zoom * Rotation * Position * ViewSize
    
    type CameraControl(_graphicsDevice : GraphicsDevice, camera) =
        member this.ViewSize = 0
        member val MainCamera = camera 
            
            

    let MakeCameraControl (_graphicsDevice : GraphicsDevice) =
        let ViewSize(width, height) as viewSize = ViewSize(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height)
        let cam = Camera(Zoom(2.0f), Rotation(0.f), Position(Vector2(float32(width) / 4.f, float32(height) / 4.f)), viewSize) 
        CameraControl(_graphicsDevice, cam)