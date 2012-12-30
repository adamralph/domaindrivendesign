module RakeHelper
  def RakeHelper.get_xunit_command(version)
    return use_mono ? File.dirname(__FILE__) + "/xunit.console.mono." + version.to_s + "." + script_extension : xunit_console(version)
  end

  def RakeHelper.get_nuget_command
    return use_mono ? File.dirname(__FILE__) + "/Nuget.Mono." + script_extension : ENV["NuGetConsolePath"] + "NuGet.exe"
  end

  def RakeHelper.use_mono
    return !is_windows || ENV["Mono"]
  end
  
  private
  def RakeHelper.xunit_console(version)
    return (ENV["XunitConsolePath_" + version.to_s] || ENV["XunitConsolePath"]) + (version == :net40 ? "xunit.console.clr4.exe" : "xunit.console.exe")
  end
  
  def RakeHelper.script_extension
    return is_windows ? "bat" : "sh"
  end

  def RakeHelper.is_windows
    return ENV["OS"] == "Windows_NT"
  end
end
