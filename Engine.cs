using CubeRender.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CubeRender;

public class Engine : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //private Triangle _triangle;
    //private Quad _quad;
    private Cube _cube;

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowAltF4 = true;
        //_graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Viewport.Width;
        //_graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Viewport.Height;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //_triangle = new Triangle(this);
        //_quad = new Quad(this);
        _cube = new Cube(this);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here
        Window.Title = $"CubeRender | FPS: {1 / gameTime.ElapsedGameTime.TotalSeconds:0.00}";
        //_triangle.Update(gameTime);
        _cube.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        //_triangle.Draw();
        //_quad.Draw();
        _cube.Draw();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}