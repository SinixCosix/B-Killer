using Mechanics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace Tests.EditMode
{
    public class TriangleTests
    {
        private readonly Vector2EqualityComparer _comparer = Vector2EqualityComparer.Instance;

        [Test]
        public void IsPointInCircumCircle()
        {
            // var triangle = new Triangle(
            //     3f, 1f,
            //     7f, 1f,
            //     5f, 4f
            // );
            // var point = new Vector2(5f, 2f);
            // Assert.AreEqual(true, triangle.IsPointInCircumCircle(point));
        }

        [Test]
        public void CalcCentroid()
        {
            // var triangle = new Triangle(
            //     2f, 1f,
            //     3f, 4f,
            //     4f, 1f);
            //
            // var actual = triangle.CalcCentroid();
            // var expected = new Vector2(3f, 2f);
            //
            // Assert.That(actual, Is.EqualTo(expected).Using(_comparer));
            // triangle = new Triangle(
            //     -2f, -1f,
            //     -3f, -4f,
            //     -4f, -1f);
            //
            // actual = triangle.CalcCentroid();
            // expected = new Vector2(-3f, -2f);
            // Assert.That(actual, Is.EqualTo(expected).Using(_comparer));
        }

        [Test]
        public void SortEdgesByCounterClockwise()
        {
            // var triangle = new Triangle(
            //     0, 0,
            //     0, 3,
            //     4, 0);
            // var actual = triangle.Edges;
            // var expected = new Vector2[3];
            // expected[0] = new Vector2(4, 0);
            // expected[1] = new Vector2(0, 3);
            // expected[2] = new Vector2(0, 0);
            //
            // for (var i = 0; i < 3; i++)
            //     Assert.True(actual[i] == expected[i]);
        }
    }
}

