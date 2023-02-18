using OpenTK; //biblioteca Para abrir janela
using OpenTK.Graphics.OpenGL; //Biblioteca dos graficos
using OpenTK.Input;

 namespace MeuJogo
{ 
    class Program : GameWindow

    {
        int xDaBola = 0;
        int yDaBola = 0;
        int TamBola = 20;
        int VelBolaX = 3;
        int VelBolaY = 3;

        int yPlayer1 = 0;
        int yPlayer2 = 0;

        int xPlayer1()
        {
            return -ClientSize.Width / 2 + LarguraDosPlayers() / 2;
        }
        int xPlayer2()
        {
            return ClientSize.Width / 2 - LarguraDosPlayers() / 2;
        }
        int LarguraDosPlayers()
        {
            return TamBola;
        }
        int AlturaDosPlayers()
        {
            return 3 * TamBola;
        }


        protected override void OnUpdateFrame(FrameEventArgs e) //Atulizamos o estado do jogo 
        {
            xDaBola = xDaBola + VelBolaX;
            yDaBola = yDaBola + VelBolaY;

            if (xDaBola + TamBola / 2 > xPlayer2() - LarguraDosPlayers() / 2
            && yDaBola - TamBola / 2 < yPlayer2 + AlturaDosPlayers() / 2
            && yDaBola + TamBola / 2 > yPlayer2 - AlturaDosPlayers() / 2)
            {
                VelBolaX = -VelBolaX;
            }

            if(xDaBola - TamBola / 2 < xPlayer1() + LarguraDosPlayers() / 2
             && yDaBola - TamBola / 2 < yPlayer1 + AlturaDosPlayers() / 2
             && yDaBola + TamBola / 2 > yPlayer1 - AlturaDosPlayers() / 2)
            {
                VelBolaX = -VelBolaX;
            }
            if (yDaBola + TamBola / 2 > ClientSize.Height / 2)
            {
                VelBolaY = -VelBolaY;
            }
            if (yDaBola - TamBola / 2 < -ClientSize.Height / 2)
            {
                VelBolaY = -VelBolaY;
            }
            if (xDaBola < -ClientSize.Width / 2 || xDaBola > ClientSize.Width / 2)
            {
                xDaBola = 0;
                yDaBola = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                yPlayer1 = yPlayer1 + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                yPlayer1 = yPlayer1 - 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                yPlayer2 = yPlayer2 + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                yPlayer2 = yPlayer2 - 5;
            }

        }

        protected override void OnRenderFrame(FrameEventArgs e) //Desenhamos o estado. E o Loop continua até o game terminar
        {

            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DesenharRetangulo(xDaBola, yDaBola, TamBola, TamBola, 1.0f, 1.0f, 0.0f);
            DesenharRetangulo(xPlayer1(), yPlayer1, LarguraDosPlayers(), AlturaDosPlayers(), 1.0f, 0.0f, 0.0f);
            DesenharRetangulo(xPlayer2(), yPlayer2, LarguraDosPlayers(), AlturaDosPlayers(), 0.0f, 0.0f, 1.0f);

            SwapBuffers();
        }

         void DesenharRetangulo (int x, int y, int largura, int altura, float r, float g, float b)

        {
            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.Quads); // x*10 = 5. Escalonamos e transaladamos o centro.
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y); 
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();
        } 

        static void Main()
        {
            new Program().Run(50); //abrir janela
        }
    }
}
 