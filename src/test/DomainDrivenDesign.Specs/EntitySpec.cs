// <copyright file="EntitySpec.cs" company="DomainDrivenDesign contributors">
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

    public static class EntitySpec
    {
        [Scenario]
        [Example(123, 234, 123, 345, true, "op_Equality")]
        [Example(123, 234, 345, 234, false, "op_Equality")]
        [Example(null, 234, 123, 234, false, "op_Equality")]
        [Example(null, 234, 123, null, false, "op_Equality")]
        [Example(123, 234, 123, 345, false, "op_Inequality")]
        [Example(123, 234, 345, 234, true, "op_Inequality")]
        [Example(123, 234, 123, 345, true, "Equals")]
        [Example(123, 234, 345, 234, false, "Equals")]
        [Example(123, 234, 123, 345, true, "Object.Equals")]
        [Example(123, 234, 345, 234, false, "Object.Equals")]
        public static void SimpleObjectComparison(
            int? leftIdentity,
            int leftFoo,
            int? rightIdentity,
            int rightFoo,
            bool expectedResult,
            string method,
            SimpleObject left,
            SimpleObject right,
            bool result)
        {
            "Given a simple object with Identity={0} and Foo='{1}'"
                .Given(() => left = new SimpleObject { Identity = leftIdentity, Foo = leftFoo });

            "And another simple object with Identity={2} and Foo='{3}'"
                .And(() => right = new SimpleObject { Identity = rightIdentity, Foo = rightFoo });

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
        public static void DifferingTypeComparison(SimpleObject left, SimpleObject right, bool result)
        {
            "Given a simple object"
                .Given(() => left = new SimpleObject());

            "And another simple object of a derived type"
                .Given(() => right = new DerivedSimpleObject());

            "When I compare the objects for equality"
                .When(() => result = left == right);

            "Then the result should be negative"
                .Then(() => result.Should().BeFalse());
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
        [Example(123, 234)]
        [Example(int.MaxValue, int.MaxValue)]
        public static void SimpleObjectHashCodeGeneration(int identity, int foo, SimpleObject simpleObject, int hashCode)
        {
            "Given a simple object with Identity={0} and Foo='{1}'"
                .Given(() => simpleObject = new SimpleObject { Identity = identity, Foo = foo });

            "When getting the hash code of the object"
                .When(() => hashCode = simpleObject.GetHashCode());

            "Then the hash code should be the hash code of the identity"
                .Then(() => hashCode.Should().Be(identity.GetHashCode()));
        }

        public class SimpleObject : Entity<int?>
        {
            public int? Identity { get; set; }

            public int Foo { get; set; }

            protected override int? GetIdentity()
            {
                return this.Identity;
            }
        }

        public class DerivedSimpleObject : SimpleObject
        {
        }
    }
}
