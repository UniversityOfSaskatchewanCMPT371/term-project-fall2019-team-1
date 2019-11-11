@Library('shared-library')_

def paramMap = [branch:'*/develop', jobName:'Full Test Suite']
buildNotifyPipe paramMap 