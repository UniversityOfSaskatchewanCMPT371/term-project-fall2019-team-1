@Library('shared-library')_

def paramMap = [branch:'develop', jobName:'Full Test Suite', jobType: 'Production']
buildNotifyPipe paramMap 