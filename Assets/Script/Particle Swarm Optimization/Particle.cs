using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public double[] position;
    public double error;
    public double[] velocity;
    public double[] bestPosition;
    public double bestError;

    public Particle(double[] pos, double err, double[] vel, double[] bestPos, double bestErr)
    {
        this.position = new double[pos.Length];
        pos.CopyTo(this.position, 0);
        this.error = err;
        this.velocity = new double[vel.Length];
        vel.CopyTo(this.velocity, 0);
        this.bestPosition = new double[bestPos.Length];
        bestPos.CopyTo(this.bestPosition, 0);
        this.bestError = bestErr;
    }

    public void SetValue(double[] pos, double err, double[] vel, double[] bestPos, double bestErr) {
        this.position = new double[pos.Length];
        pos.CopyTo(this.position, 0);
        this.error = err;
        this.velocity = new double[vel.Length];
        vel.CopyTo(this.velocity, 0);
        this.bestPosition = new double[bestPos.Length];
        bestPos.CopyTo(this.bestPosition, 0);
        this.bestError = bestErr;
    }

    public override string ToString()
    {
        string s = "";
        s += "==========================\n";
        s += "Position: ";
        for (int i = 0; i < this.position.Length; ++i)
            s += this.position[i].ToString("F4") + " ";
        s += "\n";
        s += "Error = " + this.error.ToString("F4") + "\n";
        s += "Velocity: ";
        for (int i = 0; i < this.velocity.Length; ++i)
            s += this.velocity[i].ToString("F4") + " ";
        s += "\n";
        s += "Best Position: ";
        for (int i = 0; i < this.bestPosition.Length; ++i)
            s += this.bestPosition[i].ToString("F4") + " ";
        s += "\n";
        s += "Best Error = " + this.bestError.ToString("F4") + "\n";
        s += "==========================\n";
        return s;
    }
}
