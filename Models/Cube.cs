﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CubeRender.Models;

/// <summary>
/// A class for rendering a cube
/// </summary>
public class Cube
{
    /// <summary>
    /// The vertices of the cube
    /// </summary>
    private VertexBuffer vertices;

    /// <summary>
    /// The vertex indices of the cube
    /// </summary>
    private IndexBuffer indices;

    /// <summary>
    /// The effect to use rendering the cube
    /// </summary>
    private BasicEffect effect;

    /// <summary>
    /// The game this cube belongs to
    /// </summary>
    private Game game;

    /// <summary>
    /// Constructs a cube instance
    /// </summary>
    /// <param name="game">The game that is creating the cube</param>
    public Cube(Game game)
    {
        this.game = game;
        InitializeVertices();
        InitializeIndices();
        InitializeEffect();
    }

    /// <summary>
    /// Updates the Cube
    /// </summary>
    /// <param name="gameTime"></param>
    public void Update(GameTime gameTime)
    {
        float angle = (float)gameTime.TotalGameTime.TotalSeconds;
        // Look at the cube from farther away while spinning around it
        effect.View = Matrix.CreateRotationY(angle) * Matrix.CreateLookAt(
            new Vector3(0, 5, -15),
            Vector3.Zero,
            Vector3.Up
        );
    }

    /// <summary>
    /// Draws the Cube
    /// </summary>
    public void Draw()
    {
        // Cache old rasterizer state
        RasterizerState oldState = game.GraphicsDevice.RasterizerState;

        // Disable backface culling
        RasterizerState rasterizerState = new RasterizerState();
        rasterizerState.CullMode = CullMode.None;
        game.GraphicsDevice.RasterizerState = rasterizerState;

        // apply the effect
        effect.CurrentTechnique.Passes[0].Apply();
        // set the vertex buffer
        game.GraphicsDevice.SetVertexBuffer(vertices);
        // set the index buffer
        game.GraphicsDevice.Indices = indices;
        // Draw the triangles
        game.GraphicsDevice.DrawIndexedPrimitives(
            PrimitiveType.TriangleList, // Tye type to draw
            0,                          // The first vertex to use
            0,                          // The first index to use
            12                          // the number of triangles to draw
        );

        // Restore the prior rasterizer state
        game.GraphicsDevice.RasterizerState = oldState;
    }

    /// <summary>
    /// Initialize the vertex buffer
    /// </summary>
    public void InitializeVertices()
    {
        var vertexData = new VertexPositionColor[] {
            new VertexPositionColor() { Position = new Vector3(-3,  3, -3), Color = Color.Blue },
            new VertexPositionColor() { Position = new Vector3( 3,  3, -3), Color = Color.Green },
            new VertexPositionColor() { Position = new Vector3(-3, -3, -3), Color = Color.Red },
            new VertexPositionColor() { Position = new Vector3( 3, -3, -3), Color = Color.Cyan },
            new VertexPositionColor() { Position = new Vector3(-3,  3,  3), Color = Color.Blue },
            new VertexPositionColor() { Position = new Vector3( 3,  3,  3), Color = Color.Red },
            new VertexPositionColor() { Position = new Vector3(-3, -3,  3), Color = Color.Green },
            new VertexPositionColor() { Position = new Vector3( 3, -3,  3), Color = Color.Cyan }
        };
        vertices = new VertexBuffer(
            game.GraphicsDevice,            // The graphics device to load the buffer on
            typeof(VertexPositionColor),    // The type of the vertex data
            8,                              // The count of the vertices
            BufferUsage.None                // How the buffer will be used
        );
        vertices.SetData(vertexData);
    }

    /// <summary>
    /// Initializes the index buffer
    /// </summary>
    public void InitializeIndices()
    {
        var indexData = new short[]
        {
            0, 1, 2, // Side 0
            2, 1, 3,
            4, 0, 6, // Side 1
            6, 0, 2,
            7, 5, 6, // Side 2
            6, 5, 4,
            3, 1, 7, // Side 3
            7, 1, 5,
            4, 5, 0, // Side 4
            0, 5, 1,
            3, 7, 2, // Side 5
            2, 7, 6
        };
        indices = new IndexBuffer(
            game.GraphicsDevice,            // The graphics device to use
            IndexElementSize.SixteenBits,   // The size of the index
            36,                             // The count of the indices
            BufferUsage.None                // How the buffer will be used
        );
        indices.SetData(indexData);
    }

    /// <summary>
    /// Initializes the BasicEffect to render our cube
    /// </summary>
    private void InitializeEffect()
    {
        effect = new BasicEffect(game.GraphicsDevice);
        effect.World = Matrix.Identity;
        effect.View = Matrix.CreateLookAt(
            new Vector3(0, 0, 4), // The camera position
            new Vector3(0, 0, 0), // The camera target,
            Vector3.Up            // The camera up vector
        );
        effect.Projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4,                         // The field-of-view
            game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
            0.1f, // The near plane distance
            100.0f // The far plane distance
        );
        effect.VertexColorEnabled = true;
    }
}