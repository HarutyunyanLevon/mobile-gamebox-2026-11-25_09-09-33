using System;
using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace Gamebox.Cheats.Tests
{
    // shortcut: duplicated per package so each test asmdef compiles standalone; extract when a shared test-utils assembly exists.
    public sealed class CheatsLayeringTests
    {
        private const string PackagePath = "Packages/com.gamebox.cheats";
        private const string AsmdefPattern = "*.asmdef";
        private const string OwnAssemblyName = "Gamebox.Cheats";
        private const string AssemblyNameSeparator = ".";
        private const string GameboxPrefix = "Gamebox.";
        private const string CoreAssemblyName = "Gamebox.Core";
        private const string GuidPrefix = "GUID:";
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
                    Assert.IsFalse(reference.StartsWith(GuidPrefix, StringComparison.Ordinal),
                        $"{asmdef.name} references '{reference}'; use assembly names so layering can be checked.");
                    if (reference.StartsWith(GameboxPrefix, StringComparison.Ordinal))
                    {
                        Assert.IsTrue(IsAllowed(reference), $"{asmdef.name} must not reference '{reference}' (layer rule).");
                    }
                }
            }
        }

        private static bool IsAllowed(string reference) =>
            reference == OwnAssemblyName
            || reference.StartsWith(OwnAssemblyName + AssemblyNameSeparator, StringComparison.Ordinal)
            || reference == CoreAssemblyName;
    }
}
