using System.Linq;
using Gamebox.Core;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Gamebox.App.Tests
{
    public sealed class ModuleFilterTests
    {
        private const string EnabledField = "_enabled";

        [Test]
        public void Shown_SkipsDisabledModules()
        {
            ModuleDefinition shown = Create(true);
            ModuleDefinition hidden = Create(false);

            CollectionAssert.AreEqual(new[] { shown }, ModuleFilter.Shown(new[] { shown, hidden }).ToArray());

            Object.DestroyImmediate(shown);
            Object.DestroyImmediate(hidden);
        }

        private static ModuleDefinition Create(bool enabled)
        {
            var definition = ScriptableObject.CreateInstance<ModuleDefinition>();
            var serialized = new SerializedObject(definition);
            serialized.FindProperty(EnabledField).boolValue = enabled;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            return definition;
        }
    }
}
