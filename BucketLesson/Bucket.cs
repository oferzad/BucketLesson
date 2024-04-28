using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketLesson
{
    class Bucket
    {
        //תכונות
        private int volume; //נפח הדלי בליטרים
        private int filledVolume; // כמה הדלי מלא בליטרים

        //תכונות סטטיות
        private static int totalFilledVolume = 0; //מספר הליטרים בכל הדליים במערכת
        private static int numBuckets = 0; //מספר הדליים במערכת

        //פעולה בונה
        public Bucket(int volume)
        {
            this.volume = volume;
            this.filledVolume = 0;
            Bucket.numBuckets++;
        }

        //בונה מעתיקה
        public Bucket(Bucket b)
        {
            this.volume = b.volume;
            this.filledVolume = b.filledVolume;
            Bucket.numBuckets++;
        }

        //פעולות מאחזרות
        public int GetVolume()
        {
            return this.volume;
        }

        public int GetFilledVolume()
        {
            return this.filledVolume;
        }

        // פעולות נוספות
        public int GetFreeSpace()
        {
            return this.volume - this.filledVolume;
        }

        public bool Fill(int volume)
        {
            if (volume + this.filledVolume <= this.volume)
            {
                this.filledVolume += volume;
                Bucket.totalFilledVolume += volume;
                return true;
            }
            else
                return false;
        }

        public bool FillFromBucket(Bucket b)
        {
            if (this.GetFreeSpace() >= b.filledVolume)
            {
                this.filledVolume += b.filledVolume;
                b.filledVolume = 0;
                return true;
            }
            else
                return false;
        }

        //פעולות סטטיות
        public static int GetTotalFilledVolume()
        {
            return Bucket.totalFilledVolume;
        }

        public static int GetNumOfBuckets()
        {
            return Bucket.numBuckets;
        }

        public static double GetAverageFilledVolume()
        {
            if (Bucket.numBuckets == 0)
                return 0;
            else
                return (double)Bucket.totalFilledVolume / Bucket.numBuckets;
        }

        //פעולה מתארת
        public override string ToString()
        {
            string str = $"Bucket - Volume={this.volume}, FilledVolume={this.filledVolume}";
            return str;
        }
    }
    public class TestBucket
    {
        public static void Test1()
        {
            Console.WriteLine($"Number of buckets: {Bucket.GetNumOfBuckets()}");
            Console.WriteLine($"Total water: {Bucket.GetTotalFilledVolume()}");
            Console.WriteLine($"Average filled volume: {Bucket.GetAverageFilledVolume()}");

            Bucket b1 = new Bucket(10);
            Bucket b2 = new Bucket(15);
            b1.Fill(10);
            b2.Fill(5);
            Console.WriteLine(b1);
            Console.WriteLine(b2);
            if (!b1.FillFromBucket(b2))
                b2.FillFromBucket(b1);
            Console.WriteLine(b1);
            Console.WriteLine(b2);

            Console.WriteLine($"Number of buckets: {Bucket.GetNumOfBuckets()}");
            Console.WriteLine($"Total water: {Bucket.GetTotalFilledVolume()}");
            Console.WriteLine($"Average filled volume: {Bucket.GetAverageFilledVolume()}");

            
        }

        public static void Test2()
        {
            Bucket b1 = new Bucket(10);
            Bucket b2 = new Bucket(15);
            Bucket b3 = new Bucket(20);
            b1.Fill(10);
            b2.Fill(5);
            b3.Fill(15);

            WaterReservoir wr = new WaterReservoir(10);
            wr.AddBucket(b1);
            wr.AddBucket(b2);
            wr.AddBucket(b3);

            Console.WriteLine(wr);

            Bucket b4 = new Bucket(50);

            b4.FillFromBucket(b1);
            b4.FillFromBucket(b3);
            wr.AddBucket(b4);

            Console.WriteLine(wr);

            Bucket b = wr.GetEmptierBucket();
            Console.WriteLine($"This bucket was removed! {b}");
            b = wr.GetEmptierBucket();
            Console.WriteLine($"This bucket was removed! {b}\n");
            Console.WriteLine(wr);
        }
    }
    
}
