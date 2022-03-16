using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.MapGeneration
{
    public static class BinarySpacePartitionTree
    {
        public static IEnumerable<Rect> Split()
        {
            var rects = new List<Rect>();
            var rect = new Rect(0, 0, Settings.Instance.mapSize, Settings.Instance.mapSize);
            Split(rects, rect, Settings.Instance.splitCount);

            return rects;
        }

        private static void Split(ICollection<Rect> rects, Rect rect, uint parts)
        {
            if (parts == 0)
            {
                rects.Add(rect);
                return;
            }

            var splitByVertical = SelectSplitAxis(rect);
            var splitByHorizontal = !splitByVertical;

            var settings = Settings.Instance;
            var width1 = splitByVertical
                ? Random.Range(rect.width * settings.splitRatioFrom, rect.width * settings.SplitRatioTo)
                : rect.width;
            var height1 = splitByHorizontal
                ? Random.Range(rect.height * settings.splitRatioFrom, rect.height * settings.SplitRatioTo)
                : rect.height;

            // ReSharper disable TailRecursiveCall
            var x1 = rect.x;
            var y1 = rect.y;
            var rect1 = new Rect(x1, y1, width1, height1);
            Split(rects, rect1, parts - 1);

            var width2 = splitByVertical ? rect.width - rect1.width : rect.width;
            var height2 = splitByHorizontal ? rect.height - rect1.height : rect.height;
            var x2 = splitByVertical ? x1 + width1 : x1;
            var y2 = splitByHorizontal ? y1 + height1 : y1;
            var rect2 = new Rect(x2, y2, width2, height2);
            Split(rects, rect2, parts - 1);
        }

        private static bool SelectSplitAxis(Rect rect)
        {
            bool splitByVertical;
            if (rect.width / rect.height >= 1.25)
                splitByVertical = true;
            else if (rect.height / rect.width >= 1.25)
                splitByVertical = false;
            else
                splitByVertical = Random.Range(0f, 1f) > 0.5f;

            return splitByVertical;
        }
    }
}