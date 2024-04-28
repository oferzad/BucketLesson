using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BucketLesson
{
    class WaterReservoir
    {
        //תכונות
        private Bucket[] buckets;
        private int numBuckets;
        private int minCapacity;
        private static int MAX_BUCKETS = 15;

        //פעולות בונות
        public WaterReservoir(int minCapacity)
        {
            this.numBuckets = 0;
            this.minCapacity = minCapacity;
            this.buckets = new Bucket[MAX_BUCKETS];
        }

        //בונה מעתיקה
        public WaterReservoir(WaterReservoir wr)
        {
            this.numBuckets = wr.numBuckets;
            this.minCapacity = wr.minCapacity;
            this.buckets = new Bucket[MAX_BUCKETS];

            for(int i = 0; i < this.minCapacity; i++)
            {
                this.buckets[i] = new Bucket(wr.buckets[i]);
            }
        }

        //פעולות נוספות
        public bool AddBucket(Bucket bucket)
        {
            if (this.numBuckets < MAX_BUCKETS)
            {
                this.buckets[this.numBuckets] = bucket;
                this.numBuckets++;
                return true;
            }
            else
                return false;
        }

        public bool AddNewBucket(int volume)
        {
            if (this.numBuckets < MAX_BUCKETS)
            {
                this.buckets[this.numBuckets] = new Bucket(volume);
                this.numBuckets++;
                return true;
            }
            else
                return false;
        }

        public Bucket GetEmptierBucket()
        {
            if (this.numBuckets == 0)
                return null;

            //נמצמא את הדלי עם הכי הרבה מקום פנוי
            int maxIndex = 0;
            for(int i = 1;  i < this.numBuckets; i++)
            {
                Bucket max = this.buckets[maxIndex];
                Bucket current = this.buckets[i];
                if (max.GetFreeSpace() < current.GetFreeSpace())
                {
                    maxIndex = i;
                }
            }

            //נשמור הפניה לדלי הזה
            Bucket tobeRemoved = this.buckets[maxIndex];

            //עכשיו נוציא את הדלי ממאגר המים
            for(int i=maxIndex; i < this.numBuckets - 1; i++)
            {
                this.buckets[i] = this.buckets[i + 1];
            }
            this.buckets[this.numBuckets - 1] = null;
            this.numBuckets--;

            //נחזיר את הדלי שהוצאנו
            return tobeRemoved;
        }

        public int GetNumBuckets()
        {
            return this.numBuckets;
        }

        public int GetTotalFreeSpace()
        {
            int sum = 0;
            for(int i=0; i < this.numBuckets; i++)
            {
                sum += this.buckets[i].GetFreeSpace();
            }
            return sum;
        }

        public int GetTotalFilledWater()
        {
            int sum = 0;
            for (int i = 0; i < this.numBuckets; i++)
            {
                sum += this.buckets[i].GetFilledVolume();
            }
            return sum;
        }

        public bool IsUnderMinCapacity()
        {
            int capacity = GetTotalFreeSpace();
            return capacity < this.minCapacity;
        }

        public override string ToString()
        {
            string str = $"Water Reservoir (min capacity = {this.minCapacity})\n";
            str += $"{this.numBuckets} Buckets are currently in the Reservoir\n";
            for(int i= 0; i < this.numBuckets;i++)
            {
                str += $"{i+1}. {this.buckets[i]}\n";
            }
            return str;
        }

    }
}
