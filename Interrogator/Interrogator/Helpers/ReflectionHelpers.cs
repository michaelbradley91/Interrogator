using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Interrogator.Questions;

namespace Interrogator.Helpers
{
    public static class ReflectionHelpers
    {
        public static IList<TypeInfo> AllSimpleQuestions => LazyAllSimpleQuestions.Value;

        private static readonly Lazy<IList<TypeInfo>> LazyAllSimpleQuestions = new Lazy<IList<TypeInfo>>(() =>
            Assembly.GetAssembly(typeof(IQuestion)).DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(ISimpleQuestion))).ToList());

        public static IList<TypeInfo> AllComplexQuestions => LazyAllComplexQuestions.Value;

        private static readonly Lazy<IList<TypeInfo>> LazyAllComplexQuestions = new Lazy<IList<TypeInfo>>(() =>
            Assembly.GetAssembly(typeof(IQuestion)).DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IComplexQuestion))).ToList());
    }
}
