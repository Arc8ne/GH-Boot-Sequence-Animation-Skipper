const fs = require("fs");

const path = require("path");

const ghBepInExPluginBuildUtilsLib = require("C:/Users/navee/Dev/Javascript-Projects/Grey-Hack-BepInEx-Plugin-Build-Utils-Library-NodeJS/src/index");

let pluginName = "GH-Boot-Sequence-Animation-Skipper";

let scriptFolderPath = __dirname;

let projectRootFolderPath = path.resolve(scriptFolderPath, "..");

let distFolderPath = path.resolve(projectRootFolderPath, "Dist");

let ghBepInExPluginsFolderPath = "C:/Program Files (x86)/Steam/steamapps/common/Grey Hack/BepInEx/plugins";

let ghBepInExPluginBuildUtils = new ghBepInExPluginBuildUtilsLib.GHBepInExPluginBuildUtils();

ghBepInExPluginBuildUtils.createPackage(
    ghBepInExPluginsFolderPath,
    pluginName
);

ghBepInExPluginBuildUtils.createPackage(
    distFolderPath,
    pluginName
);