require 'albacore'
require 'fileutils'
require File.expand_path('rakehelper/rakehelper', File.dirname(__FILE__))

ENV["XunitConsole_net40"] = "../packages/xunit.runners.1.9.1/tools/xunit.console.clr4.exe"
ENV["NuGetConsole"] = "../packages/NuGet.CommandLine.2.2.0/tools/NuGet.exe"

Albacore.configure do |config|
  config.log_level = :verbose
end

task :default => [ :clean, :build, :spec, :nugetpack ]

desc "Use Mono in Windows"
task :mono do
  ENV["Mono"] = "x"
  if ARGV.length == 1 && ARGV[0] = "mono"
    Rake::Task["default"].invoke
  end
end

desc "Clean solution"
task :clean do
  FileUtils.rmtree "bin"

  build = RakeHelper.use_mono ? XBuild.new : MSBuild.new
  build.properties = { :configuration => :Release }
  build.targets = [ :Clean ]
  build.solution = "DomainDrivenDesign.sln"
  build.execute
end

desc "Build solution"
task :build do
  build = RakeHelper.use_mono ? XBuild.new : MSBuild.new
  build.properties = { :configuration => :Release }
  build.targets = [ :Build ]
  build.solution = "DomainDrivenDesign.sln"
  build.execute
end

desc "Execute specs"
xunit :spec do |xunit|
  xunit.command = RakeHelper.xunit_command(:net40)
  xunit.assembly = "test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll"
  xunit.options "/html test/DomainDrivenDesign.Specs/bin/Debug/DomainDrivenDesign.Specs.dll.html"
end

desc "Create the nuget package"
nugetpack :nugetpack do |nuget|
  FileUtils.mkpath "bin"
  
  # NOTE (Adam): nuspec files can be consolidated after NuGet 2.3 is released - see http://nuget.codeplex.com/workitem/2767
  nuget.command = RakeHelper.nuget_command
  nuget.nuspec = [ "DomainDrivenDesign", ENV["OS"], "nuspec" ].select { |token| token }.join(".")  
  nuget.output = "bin"
end