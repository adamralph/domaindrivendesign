require 'rubygems'
require 'albacore'
require 'fileutils'

task :default => [ :clean, :build, :spec, :nugetpack ]

desc "Run all Mono tasks"
task :mono => [ :cleanmono, :buildmono, :specmono, :nugetpackmono ]

desc "Clean solution"
msbuild :clean do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean ]
  msb.solution = "DomainDrivenDesign.sln"
end

desc "Clean solution using Mono"
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

desc "Build solution using Mono"
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

desc "Execute specs using Mono"
xunit :specmono do |xunit|
  if ENV["OS"] == "Windows_NT"
    xunit.command = "xunitmono.bat"
  else
    xunit.command = "xunitmono.sh"
  end
  xunit.assembly = "test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll"
  xunit.options "/html test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll.html"
end

desc "Create the nuget package"
nugetpack :nugetpack do |nuget|
  FileUtils.mkpath "bin"
  
  nuget.command = "../packages/NuGet.CommandLine.2.2.0/tools/NuGet.exe"
  nuget.nuspec = "DomainDrivenDesign.Windows_NT.nuspec"
  nuget.output = "bin"
end

desc "Create the nuget package using Mono"
nugetpack :nugetpackmono do |nuget|
  FileUtils.mkpath "bin"
  
  if ENV["OS"] == "Windows_NT"
    nuget.command = "nugetmono.bat"
    nuget.nuspec = "DomainDrivenDesign.Windows_NT.nuspec"
  else
    nuget.command = "nugetmono.sh"
    nuget.nuspec = "DomainDrivenDesign.nuspec"
  end
  nuget.output = "bin"
end