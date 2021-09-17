using Inputs;
using Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using UserInterface;

namespace RPGTools
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public MouseInput MouseControl { get; set; }
        public KeyboardInputs KeyControl { get; set; }
        public MapControl MapControl { get; set; }
        public UserInterfaceControl UserInterfaceControl { get; set; }

        public static Texture2D TILE_BORDER;

        //Private variables
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            //Size of Screen
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            Window.AllowAltF4 = true;

            //Controls
            MouseControl = new MouseInput();
            KeyControl = new KeyboardInputs();
            MapControl = new MapControl(GraphicsDevice);
            UserInterfaceControl = new UserInterfaceControl(MapControl, GraphicsDevice);

            //Test
            //Texture2D test = Texture2D.FromStream(GraphicsDevice, File.OpenRead("C:\\Users\\Jakob\\Desktop\\DnDMapTest\\test1.jpg"));
            //MapControl.LoadMap(test);
            

            IsMouseVisible = true;

            KeyboardInputs.KeysClicked += OnKeyClicked;

        }

        private void OnKeyClicked(object sender, KeysClickedEvent e)
        {
            if (e.KeysClicked.Contains(Keys.Escape)) Exit();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseControl.Update();
            KeyControl.Update();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //This part is used to render the map 
            //Here we transform our coordinates
            if(MapControl._MainCam != null)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront,
                    BlendState.AlphaBlend,
                    null,
                    null,
                    null,
                    null,
                    MapControl._MainCam.GetTransformation());

                MapControl.Draw(spriteBatch, gameTime);

                spriteBatch.End();
            }

            //This part is used to render menu items to the screen, so we dont need to
            //Transform our coordinates for this part
            spriteBatch.Begin();
            UserInterfaceControl?.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
