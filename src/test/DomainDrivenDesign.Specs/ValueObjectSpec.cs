// <copyright file="ValueObjectSpec.cs" company="DomainDrivenDesign contributors">
//  Copyright (c) DomainDrivenDesign contributors. All rights reserved.
// </copyright>

namespace DomainDrivenDesign.Specs
{
    using DomainDrivenDesign;
    using FluentAssertions;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Xbehave;

    public static class ValueObjectSpec
    {
        [Scenario]
        [Example(123, "bar", 123, "bar", true, "op_Equality")]
        [Example(123, "bar", 234, "bar", false, "op_Equality")]
        [Example(123, "bar", 123, "baz", false, "op_Equality")]
        [Example(123, "bar", 234, "baz", false, "op_Equality")]
        [Example(123, "bar", 123, "bar", false, "op_Inequality")]
        [Example(123, "bar", 234, "bar", true, "op_Inequality")]
        [Example(123, "bar", 123, "baz", true, "op_Inequality")]
        [Example(123, "bar", 234, "baz", true, "op_Inequality")]
        [Example(123, "bar", 123, "bar", true, "Equals")]
        [Example(123, "bar", 234, "bar", false, "Equals")]
        [Example(123, "bar", 123, "baz", false, "Equals")]
        [Example(123, "bar", 234, "baz", false, "Equals")]
        [Example(123, "bar", 123, "bar", true, "Object.Equals")]
        [Example(123, "bar", 234, "bar", false, "Object.Equals")]
        [Example(123, "bar", 123, "baz", false, "Object.Equals")]
        [Example(123, "bar", 234, "baz", false, "Object.Equals")]
        public static void SimpleObjectComparison(
            int leftFoo, string leftBar, int rightFoo, string rightBar, bool expectedResult, string method, SimpleObject left, SimpleObject right, bool result)
        {
            "Given a simple object with Foo={0} and Bar='{1}'"
                .Given(() => left = new SimpleObject { Foo = leftFoo, Bar = leftBar });

            "And another simple object with Foo={2} and Bar='{3}'"
                .And(() => right = new SimpleObject { Foo = rightFoo, Bar = rightBar });

            "When I compare the objects using '{5}'"
                .When(() =>
                {
                    switch (method)
                    {
                        case "op_Equality":
                            result = left == right;
                            break;

                        case "op_Inequality":
                            result = left != right;
                            break;

                        case "Equals":
                            result = left.Equals(right);
                            break;

                        case "Object.Equals":
                            result = ((object)left).Equals(right);
                            break;
                    }
                });

            "Then the result should be {4}"
                .Then(() => result.Should().Be(expectedResult));
        }

        [Scenario]
        public static void NullComparison(SimpleObject simpleObject, bool result)
        {
            "Given a simple object"
                .Given(() => simpleObject = new SimpleObject());

            "When I compare the object to null for equality"
                .When(() => result = simpleObject == null);

            "Then the result should be negative"
                .Then(() => result.Should().BeFalse());
        }

        [Scenario]
        public static void ReferenceComparison(SimpleObject simpleObject, SimpleObject reference, bool result)
        {
            "Given a simple object"
                .Given(() => simpleObject = new SimpleObject());

            "And another reference to the simple object"
                .And(() => reference = simpleObject);

            "When I compare the object and the second reference for equality"
                .When(() => result = simpleObject == reference);

            "Then the result should be positive"
                .Then(() => result.Should().BeTrue());
        }

        [Scenario]
        [Example(123, "foo", 234, 1816504635)]
        [Example(int.MaxValue, "foo", int.MaxValue, 1816438804)]
        public static void SimpleObjectHashCodeGeneration(int foo, string bar, int baz, int expectedHashCode, SimpleObject simpleObject, int hashCode)
        {
            "Given a simple object with Foo={0}, Bar='{1}' and Baz={2}"
                .Given(() => simpleObject = new SimpleObject { Foo = foo, Bar = bar, Baz = baz });

            "When getting the hash code of the object"
                .When(() => hashCode = simpleObject.GetHashCode());

            "Then the hash code should be {3}"
                .Then(() => hashCode.Should().Be(expectedHashCode));
        }

        [Scenario]
        [Example(123, "foo", 234)]
        [Example(int.MaxValue, "foo", int.MaxValue)]
        public static void SimpleObjectHashCodeComparison(
            int foo, string bar, int baz, SimpleObject first, SimpleObject second, int firstHashCode, int secondHashCode)
        {
            "Given a simple object with Foo={0}, Bar='{1}' and Baz={2}"
                .Given(() => first = new SimpleObject { Foo = foo, Bar = bar, Baz = baz });

            "And another simple object with Foo={0}, Bar='{1}' and Baz={2}"
                .And(() => second = new SimpleObject { Foo = foo, Bar = bar, Baz = baz });

            "When getting the hash code of the first object"
                .When(() => firstHashCode = first.GetHashCode());

            "And getting the hash code of the second object"
                .And(() => secondHashCode = second.GetHashCode());

            "Then the hash codes should be equal"
                .Then(() => firstHashCode.Should().Be(secondHashCode));
        }

        [Scenario]
        [Example(1, 2, 3, 1, 2, 3, true)]
        [Example(1, 2, 3, 1, 2, null, false)]
        [Example(1, 2, 3, 2, 1, 3, false)]
        [Example(1, 2, 3, 1, 2, 4, false)]
        public static void ComparisonOfObjectsWithNonGenericEnumerableProperties(
            int? left0,
            int? left1,
            int? left2,
            int? right0,
            int? right1,
            int? right2,
            bool expectedResult,
            ObjectWithANonGenericEnumerableProperty left,
            ObjectWithANonGenericEnumerableProperty right,
            bool result)
        {
            "Given an object with an enumerable property consisting of {0}, {1} and {2}"
                .Given(() => left = new ObjectWithANonGenericEnumerableProperty
                    {
                        Foo = new ObjectWithANonGenericEnumerableProperty.Collection(
                            new[] { left0, left1, left2 }.Where(item => item.HasValue).Select(item => item.Value))
                    });

            "And another object with an enumerable property consisting of {3}, {4} and {5}"
                .And(() => right = new ObjectWithANonGenericEnumerableProperty
                {
                    Foo = new ObjectWithANonGenericEnumerableProperty.Collection(
                        new[] { right0, right1, right2 }.Where(item => item.HasValue).Select(item => item.Value))
                });

            "When I compare the objects for equality"
                .When(() => result = left == right);

            "Then the result should be {6}"
                .When(() => result.Should().Be(expectedResult));
        }

        [Scenario]
        [Example(1, 2, 3, 1, 2, 3, true)]
        [Example(1, 2, 3, 1, 2, null, false)]
        [Example(1, 2, 3, 2, 1, 3, false)]
        [Example(1, 2, 3, 1, 2, 4, false)]
        public static void ComparisonOfObjectsWithGenericEnumerableProperties(
            int? left0,
            int? left1,
            int? left2,
            int? right0,
            int? right1,
            int? right2,
            bool expectedResult,
            ObjectWithAGenericEnumerableProperty left,
            ObjectWithAGenericEnumerableProperty right,
            bool result)
        {
            "Given an object with a generic enumerable property consisting of {0}, {1} and {2}"
                .Given(() => left = new ObjectWithAGenericEnumerableProperty
                {
                    Foo = new ObjectWithAGenericEnumerableProperty.Collection(
                        new[] { left0, left1, left2 }.Where(item => item.HasValue).Select(item => item.Value))
                });

            "And another object with a generic enumerable property consisting of {3}, {4} and {5}"
                .And(() => right = new ObjectWithAGenericEnumerableProperty
                {
                    Foo = new ObjectWithAGenericEnumerableProperty.Collection(
                        new[] { right0, right1, right2 }.Where(item => item.HasValue).Select(item => item.Value))
                });

            "When I compare the objects for equality"
                .When(() => result = left == right);

            "Then the result should be {6}"
                .When(() => result.Should().Be(expectedResult));
        }
        
        public class SimpleObject : ValueObject
        {
            public int Foo { get; set; }

            public string Bar { get; set; }

            public int Baz { get; set; }
        }

        public class ObjectWithANonGenericEnumerableProperty : ValueObject
        {
            public Collection Foo { get; set; }

            public class Collection : IEnumerable
            {
                private readonly int[] array;

                public Collection(IEnumerable<int> items)
                {
                    this.array = items.ToArray();
                }

                public IEnumerator GetEnumerator()
                {
                    return this.array.GetEnumerator();
                }
            }
        }

        public class ObjectWithAGenericEnumerableProperty : ValueObject
        {
            public Collection Foo { get; set; }

            public class Collection : IEnumerable<int>
            {
                private readonly List<int> list;

                public Collection(IEnumerable<int> items)
                {
                    this.list = items.ToList();
                }

                IEnumerator<int> IEnumerable<int>.GetEnumerator()
                {
                    return this.list.GetEnumerator();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this.list.GetEnumerator();
                }
            }
        }

        public class DoubleGenericEnumerableObject : ValueObject
        {
            public Collection Foo { get; set; }

            public class Collection : IEnumerable<int>, IEnumerable<string>
            {
                private readonly List<int> int32List;
                private readonly List<string> stringList;

                public Collection(IEnumerable<int> int32Items, IEnumerable<string> stringItems)
                {
                    this.int32List = int32Items.ToList();
                    this.stringList = stringItems.ToList();
                }

                IEnumerator<int> IEnumerable<int>.GetEnumerator()
                {
                    return this.int32List.GetEnumerator();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this.int32List.GetEnumerator();
                }

                IEnumerator<string> IEnumerable<string>.GetEnumerator()
                {
                    return this.stringList.GetEnumerator();
                }
            }
        }
    }
}
