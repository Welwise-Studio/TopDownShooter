using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISpawner
{
    public event System.Action<int> OnNewWave;
    public Wave[] waves { get; }

}

