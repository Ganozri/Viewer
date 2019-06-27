@Library('jp') _

PelengCI([
    name : "Peleng.Medusa.Common.Checkers",
    repo : [
        url : "ssh://git@192.168.0.16:2222/kpkv2/kpkv2-user/medusa-common-checkers.git",
    ],
    tools : [
        msbuild : [
            nuget: [
                restore : true,
                update : true
            ],
            properties :[
                PlatformToolset : "v141"
            ],
            mstest: [ target: ["Medusa.Common.Checkers.Tests-Core.dll"] ],
         ]
    ],
    documentation : [
        doxygen : []
    ],
    reports : [
	pvs : [
            redmineURL : "kpkv2-user/repository/medusa-common-checkers"
        ],
    ],
    artifacts : [
        nuget : []
    ]
])

