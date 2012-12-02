require 'albacore'

task :default => [ :build ]

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