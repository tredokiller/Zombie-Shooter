using System.Linq;
using UnityEditor;
using UnityEditor.Android;
using Unity.Android.Gradle;
using Unity.Android.Gradle.Manifest;
public class ModifyProjectScript : AndroidProjectFilesModifier
{
    public override void OnModifyAndroidProjectFiles(AndroidProjectFiles projectFiles)
    {
        
        
        var elementG0 = new Element($"// Android Resolver Dependencies Start");
        projectFiles.UnityLibraryBuildGradle.Dependencies.AddElement(elementG0);
        var elementG1 = projectFiles.UnityLibraryBuildGradle.Dependencies.AddDependencyImplementationRaw("'com.google.android.gms:play-services-ads:22.3.0' // Assets/GoogleMobileAds/Editor/GoogleMobileAdsDependencies.xml:7");
        var elementG2 = projectFiles.UnityLibraryBuildGradle.Dependencies.AddDependencyImplementationRaw("'com.google.android.ump:user-messaging-platform:2.1.0' // Assets/GoogleMobileAds/Editor/GoogleUmpDependencies.xml:7");
        var elementG3 = new Element($"// Android Resolver Dependencies End");
        projectFiles.UnityLibraryBuildGradle.Dependencies.AddElement(elementG3);
        var elementG4 = projectFiles.UnityLibraryBuildGradle.Dependencies.GetElements().First(e => e.GetRaw() == $"implementation fileTree(dir: \'libs\', include: [\'*.jar\'])");
        if (elementG4?.GetRaw() != null && elementG0?.GetRaw() != null)
            elementG0?.AddElementDependency(elementG4);
        if (elementG0?.GetRaw() != null && elementG1?.GetRaw() != null)
            elementG1?.AddElementDependency(elementG0);
        if (elementG1?.GetRaw() != null && elementG2?.GetRaw() != null)
            elementG2?.AddElementDependency(elementG1);
        if (elementG2?.GetRaw() != null && elementG3?.GetRaw() != null)
            elementG3?.AddElementDependency(elementG2);
        
        var elementG5 = projectFiles.UnityLibraryBuildGradle.Android.PackagingOptions.AddExclude("('/lib/armeabi/*' + '*')");
        var elementG6 = projectFiles.UnityLibraryBuildGradle.Android.PackagingOptions.AddExclude("('/lib/mips/*' + '*')");
        var elementG7 = projectFiles.UnityLibraryBuildGradle.Android.PackagingOptions.AddExclude("('/lib/mips64/*' + '*')");
        var elementG8 = projectFiles.UnityLibraryBuildGradle.Android.PackagingOptions.AddExclude("('/lib/x86/*' + '*')");
        var elementG9 = projectFiles.UnityLibraryBuildGradle.Android.PackagingOptions.AddExclude("('/lib/x86_64/*' + '*')");
        var elementG10 = new Element($"// Android Resolver Exclusions Start");
        projectFiles.UnityLibraryBuildGradle.AddElement(elementG10);
        var elementG11 = new Element($"// Android Resolver Exclusions End");
        projectFiles.UnityLibraryBuildGradle.AddElement(elementG11);
        var elementG12 = (Android)projectFiles.UnityLibraryBuildGradle.GetElements().First(e => e is BaseBlock block && block.GetRaw() == $"ndkPath \"**NDKPATH**\"\n" +
        $"namespace \"com.unity3d.player\"\n" +
        $"compileSdkVersion **APIVERSION**\n" +
        $"buildToolsVersion \"**BUILDTOOLS**\"\n" +
        $"compileOptions {{\n" +
        $"    sourceCompatibility JavaVersion.VERSION_11\n" +
        $"    targetCompatibility JavaVersion.VERSION_11\n" +
        $"}}\n" +
        $"defaultConfig {{\n" +
        $"    minSdkVersion **MINSDKVERSION**\n" +
        $"    targetSdkVersion **TARGETSDKVERSION**\n" +
        $"    ndk {{\n" +
        $"        abiFilters **ABIFILTERS**\n" +
        $"    }}\n" +
        $"    versionCode **VERSIONCODE**\n" +
        $"    versionName \"**VERSIONNAME**\"\n" +
        $"    consumerProguardFiles \'proguard-unity.txt\'**USER_PROGUARD**\n" +
        $"    **DEFAULT_CONFIG_SETUP**\n" +
        $"}}\n" +
        $"lintOptions {{\n" +
        $"    abortOnError false\n" +
        $"}}\n" +
        $"aaptOptions {{\n" +
        $"    noCompress = **BUILTIN_NOCOMPRESS** + unityStreamingAssets.tokenize(\', \')\n" +
        $"    ignoreAssetsPattern = \"!.svn:!.git:!.ds_store:!*.scc:!CVS:!thumbs.db:!picasa.ini:!*~\"\n" +
        $"}}\n" +
        $"**PACKAGING_OPTIONS**");
        elementG12?.RemoveAllElementDependencies();
        if (elementG10?.GetRaw() != null && projectFiles.UnityLibraryBuildGradle.Android?.GetRaw() != null)
            projectFiles.UnityLibraryBuildGradle.Android?.AddElementDependency(elementG10);
        if (projectFiles.UnityLibraryBuildGradle.Dependencies?.GetRaw() != null && elementG10?.GetRaw() != null)
            elementG10?.AddElementDependency(projectFiles.UnityLibraryBuildGradle.Dependencies);
        if (projectFiles.UnityLibraryBuildGradle.Android?.GetRaw() != null && elementG11?.GetRaw() != null)
            elementG11?.AddElementDependency(projectFiles.UnityLibraryBuildGradle.Android);
        if (elementG11?.GetRaw() != null && elementG12?.GetRaw() != null)
            elementG12?.AddElementDependency(elementG11);
        
        
        var elementG13 = new Element($"# Android Resolver Properties Start");
        projectFiles.GradleProperties.AddElement(elementG13);
        var elementG14 = new Element($"android.useAndroidX=true");
        projectFiles.GradleProperties.AddElement(elementG14);
        var elementG15 = new Element($"android.enableJetifier=true");
        projectFiles.GradleProperties.AddElement(elementG15);
        var elementG16 = new Element($"# Android Resolver Properties End");
        projectFiles.GradleProperties.AddElement(elementG16);
        if (projectFiles.GradleProperties.UnityStreamingAssets?.GetRaw() != null && elementG13?.GetRaw() != null)
            elementG13?.AddElementDependency(projectFiles.GradleProperties.UnityStreamingAssets);
        if (elementG13?.GetRaw() != null && elementG14?.GetRaw() != null)
            elementG14?.AddElementDependency(elementG13);
        if (elementG14?.GetRaw() != null && elementG15?.GetRaw() != null)
            elementG15?.AddElementDependency(elementG14);
        if (elementG15?.GetRaw() != null && elementG16?.GetRaw() != null)
            elementG16?.AddElementDependency(elementG15);
        var templatedValueG19 = System.IO.Directory.GetParent(System.IO.Path.Combine(UnityEngine.Application.dataPath))?.FullName;
        
        var elementG17 = new Element($"// Android Resolver Repos Start");
        projectFiles.GradleSettings.DependencyResolutionManagement.Repositories.AddElement(elementG17);
        var elementG18 = new Element($"def unityProjectPath = $/file:///{templatedValueG19}/$.replace(\"\\\\\", \"/\")");
        projectFiles.GradleSettings.DependencyResolutionManagement.Repositories.AddElement(elementG18);
        var elementG20 = new Block("maven");
        elementG20.SetRaw($"url \"https://maven.google.com/\" // Assets/GoogleMobileAds/Editor/GoogleMobileAdsDependencies.xml:7, Assets/GoogleMobileAds/Editor/GoogleUmpDependencies.xml:7");
        projectFiles.GradleSettings.DependencyResolutionManagement.Repositories.AddElement(elementG20);
        var elementG21 = new Element($"mavenLocal()");
        projectFiles.GradleSettings.DependencyResolutionManagement.Repositories.AddElement(elementG21);
        var elementG22 = new Element($"// Android Resolver Repos End");
        projectFiles.GradleSettings.DependencyResolutionManagement.Repositories.AddElement(elementG22);
        var elementG23 = projectFiles.GradleSettings.DependencyResolutionManagement.Repositories.GetElements().First(e => e.GetRaw() == $"mavenCentral()");
        var elementG24 = (Block)projectFiles.GradleSettings.DependencyResolutionManagement.Repositories.GetElements().First(e => e is BaseBlock block && block.GetName() == "flatDir");
        elementG24?.RemoveAllElementDependencies();
        if (elementG23?.GetRaw() != null && elementG17?.GetRaw() != null)
            elementG17?.AddElementDependency(elementG23);
        if (elementG17?.GetRaw() != null && elementG18?.GetRaw() != null)
            elementG18?.AddElementDependency(elementG17);
        if (elementG18?.GetRaw() != null && elementG20?.GetRaw() != null)
            elementG20?.AddElementDependency(elementG18);
        if (elementG20?.GetRaw() != null && elementG21?.GetRaw() != null)
            elementG21?.AddElementDependency(elementG20);
        if (elementG21?.GetRaw() != null && elementG22?.GetRaw() != null)
            elementG22?.AddElementDependency(elementG21);
        if (elementG22?.GetRaw() != null && elementG24?.GetRaw() != null)
            elementG24?.AddElementDependency(elementG22);

    }
}
