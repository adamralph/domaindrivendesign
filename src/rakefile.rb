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
output :output => [ :build ] do |out|
  FileUtils.rmtree "bin"
  FileUtils.mkpath "bin/lib/net35"
  
  out.from "DomainDrivenDesign/bin/Release"
  out.to "bin/lib/net35"
  out.file "DomainDrivenDesign.dll"
  out.file "DomainDrivenDesign.xml"
end

desc "Build"
msbuild :build => :clean do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Build ]
  msb.solution = "DomainDrivenDesign.sln"
end

desc "Clean"
msbuild :clean do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean ]
  msb.solution = "DomainDrivenDesign.sln"
end