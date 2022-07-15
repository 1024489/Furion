# �ų� Furion �� Furion.Pure ������ԭ������������������ Furion.[Pure].Extras.DependencyModel.CodeAnalysis ��չ��
#
# �������
Param(
    # �汾��
    [string] $version,
    # Nuget APIKey
    [string] $apikey
)

if ($version -eq $null -or $version -eq "")
{
    Write-Error "����ָ���汾��";
    return;
}

Write-Warning "���ڷ��� framework Ŀ¼ Nuget ��......";

# ���� .\framework\nupkgs ������Ŀ¼
cd .\framework\nupkgs;
$framework_nupkgs = Get-ChildItem -Filter *.nupkg;

# �������� *.nupkg �ļ�
for ($i = 0; $i -le $framework_nupkgs.Length - 1; $i++){
    $item = $framework_nupkgs[$i];

    # �ų� Furion �� Furion.Pure ������ԭ������������������ Furion.[Pure].Extras.DependencyModel.CodeAnalysis ��չ��
    if ($item.Name -ne "Furion.$version.nupkg" -and $item.Name -ne "Furion.Pure.$version.nupkg")
    {
        $nupkg = $item.FullName;
        $snupkg = $nupkg.Replace(".nupkg", ".snupkg");

        Write-Output "-----------------";
        $nupkg;

        # ������ nuget.org ƽ̨
        dotnet nuget push $nupkg --skip-duplicate --api-key $apikey --source https://api.nuget.org/v3/index.json;
        dotnet nuget push $snupkg --skip-duplicate --api-key $apikey --source https://api.nuget.org/v3/index.json;

        Write-Output "-----------------";
    }
}

# �ص���Ŀ��Ŀ¼
cd ../../;

Write-Warning "�����ɹ�";