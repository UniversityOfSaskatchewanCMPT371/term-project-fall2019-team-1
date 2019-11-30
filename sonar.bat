SonarScanner.MSBuild.exe begin /k:"UniversityOfSaskatchewanCMPT371_term-project-fall2019-team-1" /o:"universityofsaskatchewancmpt371" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="b71864c9fe8e390abb6927ac9bf1b17ed7aa2f57" /d:sonar.cs.opencover.reportsPaths="CodeCoverage/**" /d:sonar.exclusions="Assets/Imported/**,Assets/plugins/**,Assets/Resources/**,Assets/Tests-EditMode/**,Assets/Tests-PlayMode/**"

MSBuild.exe term-project-fall2019-team-1.sln

SonarScanner.MSBuild.exe end /d:sonar.login="b71864c9fe8e390abb6927ac9bf1b17ed7aa2f57"