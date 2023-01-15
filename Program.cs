using cgame;

ConfigureConsole();
Main main = new Main(Console.Out, Console.In);
main.Start();

while(true)
{
    main.Update();
}

void ConfigureConsole()
{
    CustomConsole customConsole = new CustomConsole();
    customConsole.SetConsole();
}
