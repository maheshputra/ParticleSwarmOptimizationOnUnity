using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSOManager : MonoBehaviour
{
    int dimension = 2;//mendefinisikan dimensi problem(2 = 2 dimensi)
    static int numParticles = 5;
    int maxEpoch = 1000;
    double exitError = 0f;
    double minX = -10f;
    double maxX = 10f;
    Vector2 bestPosition;
    double bestError;

    // Start is called before the first frame update
    void Start()
    {
        bestPosition = Solve(dimension, numParticles, minX, maxX, maxEpoch, exitError);
        bestError = GetError(bestPosition);
    }

    private static double GetError(Vector2 bestPosition)
    {

        double trueMin = -0.42888194; // true min for z = x * exp(-(x^2 + y^2))
        double z = bestPosition.x * Math.Exp(-((bestPosition.x * bestPosition.x) + (bestPosition.y * bestPosition.y)));
        return (z - trueMin) * (z - trueMin); // squared diff
    }

    // Update is called once per frame
    void Update()
    {

    }

    private static Vector2 Solve(int _dimension, int _numParticles, double _minX, double _maxX, int _maxEpoch, double _exitError)
    {
        System.Random rnd = new System.Random(0);
        PartikelPSO[] swarm = new PartikelPSO[numParticles];
        Vector2 bestGlobalPosition = new Vector2();
        double bestGlobalError = double.MaxValue;
        //swarm init
        for (int i = 0; i < swarm.Length; i++)
        {
            Vector2 randomPosition = new Vector2();

            randomPosition.x = (((float)_maxX - (float)_minX) * (float)rnd.NextDouble()) + (float)_minX;
            randomPosition.y = (((float)_maxX - (float)_minX) * (float)rnd.NextDouble()) + (float)_minX;

            double error = GetError(randomPosition);
            Vector2 randomVelocity = new Vector2();


            double lo = _minX * 0.1;
            double hi = _maxX * 0.1;
            randomVelocity.x = ((float)hi - (float)lo) * (float)rnd.NextDouble() + (float)lo;
            randomVelocity.y = ((float)hi - (float)lo) * (float)rnd.NextDouble() + (float)lo;

            swarm[i] = new PartikelPSO(randomPosition, error, randomVelocity, randomPosition, error);

            if (swarm[i].error < bestGlobalError)
            {
                bestGlobalError = swarm[i].error;
                swarm[i].transform.position = bestGlobalPosition;
            }
        }

        //initialize
        double w = 0.729; // inertia weight. see http://ieeexplore.ieee.org/stamp/stamp.jsp?arnumber=00870279
        double c1 = 1.49445; // cognitive/local weight
        double c2 = 1.49445; // social/global weight
        double r1, r2; // cognitive and social randomizations
        double probDeath = 0.01;
        int epoch = 0;

        Vector2 newVelocity = new Vector2();
        Vector2 newPosition = new Vector2();
        double newError;
        //main loop
        while (epoch < _maxEpoch)
        {
            PartikelPSO currPosition = new PartikelPSO();
            for (int i = 0; i < swarm.Length; i++)
            {
                currPosition = swarm[i];
                r1 = rnd.NextDouble();
                r2 = rnd.NextDouble();
                newVelocity.x = ((float)w * currPosition.velocity.x) +
                    ((float)c1 * (float)r1 * (currPosition.bestPosition.x - currPosition.transform.position.x)) +
                    ((float)c2 * (float)r2 * (currPosition.bestPosition.x - currPosition.transform.position.x));

                newVelocity.y = ((float)w * currPosition.velocity.y) +
                    ((float)c1 * (float)r1 * (currPosition.bestPosition.y - currPosition.transform.position.y)) +
                    ((float)c2 * (float)r2 * (currPosition.bestPosition.y - currPosition.transform.position.y));

                newVelocity = currPosition.velocity;


                newPosition = (Vector2)currPosition.transform.position + newVelocity;//belum yakin
                if (newPosition.x < _maxX)
                {
                    newPosition.x = (float)_minX;
                }
                else if (newPosition.x > _maxX)
                {
                    newPosition.x = (float)_maxX;
                }
                if (newPosition.y < _maxX)
                {
                    newPosition.y = (float)_minX;
                }
                else if (newPosition.y > _maxX)
                {
                    newPosition.y = (float)_maxX;
                }

                newPosition = currPosition.transform.position;//belum yakin

                newError = GetError(newPosition);
                currPosition.error = newError;
                if (newError < currPosition.bestError)
                {
                    newPosition = bestGlobalPosition;
                    currPosition.bestError = newError;
                }

                if (newError < bestGlobalError)
                {
                    newPosition = bestGlobalPosition;
                    currPosition.bestError = newError;
                }

                double die = rnd.NextDouble();
                if (die < probDeath)
                {
                    float newPosX = ((float)_maxX - (float)_minX) * (float)rnd.NextDouble() + (float)_minX;
                    float newPOsY = ((float)_maxX - (float)_minX) * (float)rnd.NextDouble() + (float)_minX;

                    Vector2 tempPos = new Vector2(newPosX, newPOsY);
                    currPosition.transform.position = tempPos;
                    currPosition.error = GetError(currPosition.transform.position);
                    currPosition.transform.position = currPosition.bestPosition;
                    currPosition.bestError = currPosition.error;
                }
            }
            epoch++;
        }//end loop

        Vector2 result = new Vector2();
        result = bestGlobalPosition;

        Debug.Log(result);
        return result;
    }
}
