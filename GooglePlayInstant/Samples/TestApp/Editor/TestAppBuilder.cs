// Copyright 2018 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using GooglePlayInstant.Editor;
using UnityEditor;

namespace GooglePlayInstant.Samples.TestApp.Editor
{
    /// <summary>
    /// Provides a method to build the plugin TestApp from the command line.
    /// </summary>
    public static class TestAppBuilder
    {
        private static readonly string[] TestScenePaths = {"Assets/TestApp/Scenes/TestScene.unity"};

        public static void Build()
        {
            PlayerSettings.applicationIdentifier = "com.google.android.instantapps.samples.unity.testapp";
            PlayerSettings.companyName = "Google";
            PlayerSettings.productName = "testapp";

            CommandLineBuilder.ConfigureProject(TestScenePaths);

            // Build APK.
            var apkPath = CommandLineBuilder.GetApkPath();
            var buildPlayerOptions = PlayInstantBuilder.CreateBuildPlayerOptions(apkPath, BuildOptions.None);
            if (!PlayInstantBuilder.BuildAndSign(buildPlayerOptions))
            {
                throw new Exception("APK build failed");
            }

            // Also Build an AAB to test Android App Bundle build.
            var aabPath = apkPath.Substring(0, apkPath.Length - 3) + "aab";
            if (!AppBundlePublisher.Build(aabPath))
            {
                throw new Exception("AAB build failed");
            }
        }
    }
}