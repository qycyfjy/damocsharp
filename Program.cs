using Damo;

var damo = DmSoft.LoadAndUnlock();
Console.WriteLine(damo.Ver());
var handle = damo.FindWindow("扫雷", "扫雷");
var bind = damo.BindWindowEx(handle, "normal", "windows", "normal", "", 0);
if (bind != 1)
{
    Console.WriteLine("failed to bind window");
    Environment.Exit(1);
}

var mines = damo.ReadInt(handle, "01005330", 0);
var width = damo.ReadInt(handle, "01005334", 0);
var height = damo.ReadInt(handle, "01005338", 0);
Console.WriteLine($"{mines} {width} {height}");
var Sweep = (int x, int y) =>
{
    var tx = 1 + 16 * x;
    var ty = 50 + 16 * y;
    // var pos = damo.ClientToScreen(handle, tx, ty);
    // damo.MoveTo(pos.X, pos.Y);
    damo.MoveTo(tx, ty);
    damo.LeftClick();
};

Thread.Sleep(3000);

var boardBase = 0x1005340;
for (int y = 1; y <= height; y++)
{
    for (int x = 1; x <= width; x++)
    {
        var v = damo.ReadData(handle, (boardBase + x + 32 * y).ToString("X4"), 1);
        if (v == "0F")
        {
            Sweep(x, y);
        }
    }
}
