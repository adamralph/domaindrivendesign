require 'rubygems'
require 'albacore'

module RakeHelper
  def RakeHelper.get_build_task
    if use_mono
      return XBuild.new
    else
      return MSBuild.new
    end
  end

  def RakeHelper.get_xunit_command_net40
    if use_mono
      if ENV["OS"] == "Windows_NT"
        return File.dirname(__FILE__) + "/xunitmono40.bat"
      else
        return File.dirname(__FILE__) + "/xunitmono40.sh"
      end
    else
      return ENV["XunitConsole40"]
    end
  end

  def RakeHelper.get_nuget_command
    if use_mono
      if ENV["OS"] == "Windows_NT"
        return File.dirname(__FILE__) + "/nugetmono.bat"
      else
        return File.dirname(__FILE__) + "/nugetmono.sh"
      end
    else
      return ENV["NuGetConsole"]
    end
  end

  private
  def RakeHelper.use_mono
    return ENV["OS"] != "Windows_NT" || ENV["Mono"]
  end

end