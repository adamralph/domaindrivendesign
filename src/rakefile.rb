require 'rubygems'
require 'albacore'
require 'fileutils'

task :default => [ :clean, :build, :spec, :nugetpack ]

desc "Forces use of mono for all subsequent tasks"
task :mono do
  ENV["MONO"] = "1"
end

desc "Clean solution"
task :clean do
  FileUtils.rmtree "bin"

  if use_mono then
    build = XBuild.new
  else
    build = MSBuild.new
  end
  
  build.properties = { :configuration => :Release }
  build.targets = [ :Clean ]
  build.solution = "DomainDrivenDesign.sln"
  build.execute
end

desc "Build solution"
task :build do
  if use_mono then
    build = XBuild.new
  else
    build = MSBuild.new
  end
  
  build.properties = { :configuration => :Release }
  build.targets = [ :Build ]
  build.solution = "DomainDrivenDesign.sln"
  build.execute
end

desc "Execute specs"
xunit :spec do |xunit|
  if use_mono then
    if ENV["OS"] == "Windows_NT"
      xunit.command = "xunitmono.bat"
    else
      xunit.command = "xunitmono.sh"
    end
  else
    xunit.command = "../packages/xunit.runners.1.9.1/tools/xunit.console.clr4.exe"
  end if
  
  xunit.assembly = "test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll"
  xunit.options "/html test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll.html"
end

desc "Create the nuget package"
nugetpack :nugetpack do |nuget|
  FileUtils.mkpath "bin"
  
  if use_mono then
    if ENV["OS"] == "Windows_NT"
      nuget.command = "nugetmono.bat"
    else
      nuget.command = "nugetmono.sh"
    end
  else
    nuget.command = "../packages/NuGet.CommandLine.2.2.0/tools/NuGet.exe"
  end

  # NOTE (Adam): nuspec files can be consolidated after NuGet 2.3 is released - see http://nuget.codeplex.com/workitem/2767
  if ENV["OS"] == "Windows_NT"
    nuget.nuspec = "DomainDrivenDesign.Windows_NT.nuspec"
  else
    nuget.nuspec = "DomainDrivenDesign.nuspec"
  end
  
  nuget.output = "bin"
end

def use_mono()
  return ENV["OS"] != "Windows_NT" || ENV["MONO"]
end