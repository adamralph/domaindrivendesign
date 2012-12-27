require 'rubygems'
require 'albacore'
require 'fileutils'

task :default => [ :clean, :build, :spec, :nugetpack ]
task :mono => [ :cleanmono, :buildmono, :specmono ]

desc "Clean solution"
msbuild :clean do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean ]
  msb.solution = "DomainDrivenDesign.sln"
end

desc "Clean solution"
xbuild :cleanmono do |xb|
  xb.properties = { :configuration => :Release }
  xb.targets = [ :Clean ]
  xb.solution = "DomainDrivenDesign.sln"
end

desc "Build solution"
msbuild :build do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Build ]
  msb.solution = "DomainDrivenDesign.sln"
end

desc "Build solution"
xbuild :buildmono do |xb|
  ENV["CustomBuildSystem"] = "xbuild"
  xb.properties = { :configuration => :Release }
  xb.targets = [ :Build ]
  xb.solution = "DomainDrivenDesign.sln"
end

desc "Execute specs"
xunit :spec do |xunit|
  xunit.command = "../packages/xunit.runners.1.9.1/tools/xunit.console.clr4.exe"
  xunit.assembly = "test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll"
  xunit.html_output = "test/DomainDrivenDesign.Specs/bin/Debug"
end

desc "Execute specs"
xunit :specmono do |xunit|
  if ENV["OS"] == "Windows_NT"
    xunit.command = "xunitmono.bat"
  else
    xunit.command = "xunitmono.sh"
  end
  xunit.assembly = "test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll"
end

desc "Create the nuget package"
nugetpack :nugetpack do |nuget|
  FileUtils.mkpath "bin"
  
  nuget.command = "../packages/NuGet.CommandLine.2.1.2/tools/NuGet.exe"
  nuget.nuspec = "DomainDrivenDesign.nuspec"
  nuget.output = "bin"
end