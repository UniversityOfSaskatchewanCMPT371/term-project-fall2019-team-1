@Library('shared-library')_

def paramMap = [branch: "${env.BRANCH}", jobName: 'Doxygen Writer', slackChannel: "${env.SLACK_ID}"]
buildNotifyPipe paramMap