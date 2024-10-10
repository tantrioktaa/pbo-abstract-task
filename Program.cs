using System;

class Program
{
    static void Main(string[] args)
    {
        Robot robot1 = new RobotBiasa("Spiderman", 100, 30, 20);
        Robot robot2 = new RobotBiasa("Mysterio", 120, 25, 25);

        BosRobot bos = new BosRobot("Iron Man", 300, 50);

        robot1.CetakInformasi();
        robot2.CetakInformasi();
        bos.CetakInformasi();
        Console.WriteLine();

        robot1.Serang(robot2);
        robot2.Serang(robot1);
        Console.WriteLine();

        robot2.Serang(bos);
        bos.CetakInformasi();
        Console.WriteLine();

        // Gunakan kemampuan
        robot2.GunakanKemampuan(new SeranganListrik());
        Console.WriteLine();
        robot2.GunakanKemampuan(new SeranganPlasma());
        Console.WriteLine();
        robot1.GunakanKemampuan(new PertahananSuper());
        Console.WriteLine();

        robot1.CetakInformasi();
        robot2.CetakInformasi();
        bos.CetakInformasi();
        Console.WriteLine();

        robot1.GunakanKemampuan(new Perbaikan());
        robot2.GunakanKemampuan(new Perbaikan());
        bos.GunakanKemampuan(new Perbaikan());
        Console.WriteLine();

        robot1.CetakInformasi();
        robot2.CetakInformasi();
        bos.CetakInformasi();
    }
}

abstract class Robot
{
    public string nama;
    public int energi;
    public int armor;
    public int serangan;

    public Robot(string nama, int energi, int armor, int serangan)
    {
        this.nama = nama;
        this.energi = energi;
        this.armor = armor;
        this.serangan = serangan;
    }

    public abstract void Serang(Robot target);

    public void GunakanKemampuan(IKemampuan kemampuan)
    {
        if (kemampuan.DapatDigunakan())
        {
            Console.WriteLine($"{nama} menggunakan kemampuan: {kemampuan.NamaKemampuan()}");
            kemampuan.Aktivasi(this);
            kemampuan.SetCooldown();
        }
        else
        {
            Console.WriteLine($"{nama} tidak bisa menggunakan {kemampuan.NamaKemampuan()} karena dalam cooldown.");
        }
    }

    public void CetakInformasi()
    {
        Console.WriteLine($"Robot: {nama}, Energi: {energi}, Armor: {armor}, Serangan: {serangan}");
    }
}

class RobotBiasa : Robot
{
    public RobotBiasa(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }

    public override void Serang(Robot target)
    {
        Console.WriteLine($"{nama} menyerang {target.nama} dengan kekuatan {serangan}!");
        int damage = serangan - target.armor;
        if (damage < 0) damage = 0;
        target.energi -= damage;
        if (target.energi < 0) target.energi = 0;
    }
}

class BosRobot : Robot
{
    public BosRobot(string nama, int energi, int armor)
        : base(nama, energi, armor, 50) { }

    public override void Serang(Robot target)
    {
        Console.WriteLine($"{nama} menyerang {target.nama} dengan kekuatan {serangan}!");
        int damage = serangan - target.armor;
        if (damage < 0) damage = 0;
        target.energi -= damage;
        if (target.energi < 0) target.energi = 0;
    }

    public void Diserang(Robot penyerang)
    {
        Console.WriteLine($"{penyerang.nama} menyerang bos {nama} dengan kekuatan {penyerang.serangan}!");
        int damage = penyerang.serangan - armor;
        if (damage < 0) damage = 0;
        energi -= damage;
        if (energi < 0) energi = 0;
        if (energi == 0)
        {
            Mati();
        }
    }

    public void Mati()
    {
        Console.WriteLine($"{nama} telah dikalahkan!");
    }
}

interface IKemampuan
{
    string NamaKemampuan();
    void Aktivasi(Robot robot);
    bool DapatDigunakan();
    void SetCooldown();
}

class Perbaikan : IKemampuan
{
    private int cooldown = 0;
    private const int cooldownTime = 1;

    public string NamaKemampuan()
    {
        return "Perbaikan";
    }

    public void Aktivasi(Robot robot)
    {
        robot.energi += 30;
        Console.WriteLine($"Energi {robot.nama} dipulihkan sebesar 30 energi. Total energi sekarang: {robot.energi}");
    }

    public bool DapatDigunakan()
    {
        return cooldown <= 0;
    }

    public void SetCooldown()
    {
        cooldown = cooldownTime;
        Console.WriteLine($"{NamaKemampuan()} dalam cooldown selama {cooldownTime} turn.");
    }
}

class SeranganListrik : IKemampuan
{
    private int cooldown = 0;
    private const int cooldownTime = 2;

    public string NamaKemampuan()
    {
        return "Serangan Listrik";
    }

    public void Aktivasi(Robot robot)
    {
        Console.WriteLine($"{robot.nama} menggunakan Serangan Listrik! Gerakan lawan mulai lambat.");
    }

    public bool DapatDigunakan()
    {
        return cooldown <= 0;
    }

    public void SetCooldown()
    {
        cooldown = cooldownTime;
        Console.WriteLine($"{NamaKemampuan()} dalam cooldown selama {cooldownTime} turn.");
    }
}

class SeranganPlasma : IKemampuan
{
    private int cooldown = 0;
    private const int cooldownTime = 3;

    public string NamaKemampuan()
    {
        return "Serangan Plasma";
    }

    public void Aktivasi(Robot robot)
    {
        Console.WriteLine($"{robot.nama} menembakkan Serangan Plasma yang menembus armor lawan!");
    }

    public bool DapatDigunakan()
    {
        return cooldown <= 0;
    }

    public void SetCooldown()
    {
        cooldown = cooldownTime;
        Console.WriteLine($"{NamaKemampuan()} dalam cooldown selama {cooldownTime} turn.");
    }
}

class PertahananSuper : IKemampuan
{
    private int cooldown = 0;
    private const int cooldownTime = 4;

    public string NamaKemampuan()
    {
        return "Pertahanan Super";
    }

    public void Aktivasi(Robot robot)
    {
        robot.armor += 20;
        Console.WriteLine($"{robot.nama} meningkatkan 20 armor. Total armor sekarang: {robot.armor}");
    }

    public bool DapatDigunakan()
    {
        return cooldown <= 0;
    }

    public void SetCooldown()
    {
        cooldown = cooldownTime;
        Console.WriteLine($"{NamaKemampuan()} dalam cooldown selama {cooldownTime} turn.");
    }
}
