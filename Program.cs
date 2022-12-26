using cgame;

ConfigureConsole();
Main main = new Main(Console.Out);
main.Start();

while(true)
{
    Console.ReadLine();
}

void ConfigureConsole()
{
    CustomConsole customConsole = new CustomConsole();
    customConsole.SetConsoleFont();
}
Console.WriteLine("Hello, World!");
