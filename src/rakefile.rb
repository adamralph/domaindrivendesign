require 'albacore'
require 'fileutils'

task :default => [ :nugetpack ]

desc "Create the nuget package"
nugetpack :nugetpack => [ :output ] do |nuget|
  nuget.command     = "../packages/NuGet.CommandLine.2.1.2/tools/NuGet.exe"
  nuget.nuspec      = "DomainDrivenDesign.nuspec"
  nuget.base_folder = "bin"
  nuget.output      = "bin"
end

desc "Prepare the output folder"
output :output => [ :spec ] do |out|
  FileUtils.rmtree "bin"
  FileUtils.mkpath "bin/lib/net40"
  
  out.from "DomainDrivenDesign/bin/Release"
  out.to "bin/lib/net40"
  out.file "DomainDrivenDesign.dll"
  out.file "DomainDrivenDesign.xml"
end

desc "Execute specs"
xunit :spec => :build do |xunit|
  xunit.command = "../packages/xunit.runners.1.9.1/tools/xunit.console.clr4.exe"
  xunit.assembly = "test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll"
  xunit.options '/html test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.html'
end

desc "Build solution"
msbuild :build => :clean do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Build ]
  msb.solution = "DomainDrivenDesign.sln"
end

desc "Clean solution"
msbuild :clean do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean ]
  msb.solution = "DomainDrivenDesign.sln"
end