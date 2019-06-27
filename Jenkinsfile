@Library('jp') _

def projectName = 'Peleng.Medusa.Analyze1553B'
def ssh_url = 'ssh://git@192.168.0.16/kpkv2/kpkv2-user/medusa-apps-analyze1553b.git'

PelengCI([
    name : projectName,
    repo : [
        url : ssh_url,
    ],
    tools : [
        msbuild : [
            nuget : [
                restore : true,
                update : true
            ],
            properties : [
                PlatformToolset : "v141"
            ],
            mstest: [ target: ["Peleng.Medusa.Analyze1553B.Common.Tests.dll"] ],
        ]
    ],
    documentation : [
        doxygen : []
    ],
    reports : [
        pvs : [],
    ],
])
