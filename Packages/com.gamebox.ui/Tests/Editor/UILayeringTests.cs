using System;
using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace Gamebox.UI.Tests
{
    // shortcut: duplicated per package so each test asmdef compiles standalone; extract when a shared test-utils assembly exists.
    public sealed class UILayeringTests
    {
        private const string PackagePath = "Packages/com.gamebox.ui";
        private const string AsmdefPattern = "*.asmdef";
        private const string OwnAssemblyName = "Gamebox.UI";
        private const string AssemblyNameSeparator = ".";
        private const string GameboxPrefix = "Gamebox.";
        private const string CoreAssemblyName = "Gamebox.Core";
        private const string GuidPrefix = "GUID:";
        private const string ForbiddenGuidReference = "GUID:00000000000000000000000000000000";
        private const string ForbiddenSiblingReference = "Gamebox.Economy";
        private const int ExpectedAsmdefCount = 3;

        // Field names match the asmdef JSON keys, so they break the usual naming rule on purpose.
        [Serializable]
        private sealed class AsmdefData
        {
            public string name;
            public string[] references;
        }

        [Test]
        public void Asmdefs_ReferenceOnlyAllowedGameboxAssemblies()
        {
            string[] paths = Directory.GetFiles(PackagePath, AsmdefPattern, SearchOption.AllDirectories);
            Assert.AreEqual(ExpectedAsmdefCount, paths.Length, $"Unexpected asmdef count under {PackagePath}.");

            foreach (string path in paths)
            {
                AsmdefData asmdef = JsonUtility.FromJson<AsmdefData>(File.ReadAllText(path));
                foreach (string reference in asmdef.references ?? Array.Empty<string>())
                {
                    string violation = FindViolation(reference);
                    Assert.IsNull(violation, $"{asmdef.name} {violation}");
                }
            }
        }

        // Proves the check can fail; a check that never fails would pass silently.
        [TestCase(ForbiddenGuidReference)]
        [TestCase(ForbiddenSiblingReference)]
        public void FindViolation_RejectsForbiddenReference(string reference)
        {
            Assert.IsNotNull(FindViolation(reference));
        }

        private static string FindViolation(string reference)
        {
            if (reference.StartsWith(GuidPrefix, StringComparison.Ordinal))
            {
                return $"references '{reference}'; use assembly names so layering can be checked.";
            }

            if (reference.StartsWith(GameboxPrefix, StringComparison.Ordinal) && !IsAllowed(reference))
            {
                return $"must not reference '{reference}' (layer rule).";
            }

            return null;
        }

        private static bool IsAllowed(string reference) =>
            reference == OwnAssemblyName
            || reference.StartsWith(OwnAssemblyName + AssemblyNameSeparator, StringComparison.Ordinal)
            || reference == CoreAssemblyName;
    }
}
