require 'rubygems'
require 'albacore'
require 'fileutils'

ENV["XunitConsole40"] = "../packages/xunit.runners.1.9.1/tools/xunit.console.clr4.exe"
ENV["NuGetConsole"] = "../packages/NuGet.CommandLine.2.2.0/tools/NuGet.exe"

task :default => [ :clean, :build, :spec, :nugetpack ]

desc "Clean solution"
task :clean do
  FileUtils.rmtree "bin"

  build = get_build_task
  build.properties = { :configuration => :Release }
  build.targets = [ :Clean ]
  build.solution = "DomainDrivenDesign.sln"
  build.execute
end

desc "Build solution"
task :build do
  build = get_build_task
  build.properties = { :configuration => :Release }
  build.targets = [ :Build ]
  build.solution = "DomainDrivenDesign.sln"
  build.execute
end

desc "Execute specs"
xunit :spec do |xunit|
  xunit.command = get_xunit_command_net40
  xunit.assembly = "test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll"
  xunit.options "/html test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll.html"
end

desc "Create the nuget package"
nugetpack :nugetpack do |nuget|
  FileUtils.mkpath "bin"
  
  nuget.command = get_nuget_command
  nuget.nuspec = [ "DomainDrivenDesign", ENV["OS"], "nuspec" ].select { |token| token }.join(".")  
  nuget.output = "bin"
end

def use_mono()
  return ENV["OS"] != "Windows_NT" || ENV["Mono"]
end

def get_build_task()
  if use_mono
    return XBuild.new
  else
    return MSBuild.new
  end
end

def get_xunit_command_net40()
  if use_mono
    if ENV["OS"] == "Windows_NT"
      return "xunitmono40.bat"
    else
      return "xunitmono40.sh"
    end
  else
    return ENV["XunitConsole40"]
  end
end

def get_nuget_command()
  if use_mono
    if ENV["OS"] == "Windows_NT"
      return "nugetmono.bat"
    else
      return "nugetmono.sh"
    end
  else
    return ENV["NuGetConsole"]
  end
end