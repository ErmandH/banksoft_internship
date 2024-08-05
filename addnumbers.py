# Definition for singly-linked list.
# class ListNode(object):
#     def __init__(self, val=0, next=None):
#         self.val = val
#         self.next = next
class Solution(object):
    def addTwoNumbers(self, l1, l2):
        """
        :type l1: ListNode
        :type l2: ListNode
        :rtype: ListNode
        """
        sumOfVal = l1.val + l2.val
        carry =  1 if sumOfVal >  9 else 0
        mainNode = ListNode(sumOfVal % 10)
        startNode = mainNode
        while True:
            l1 = l1.next if l1 != None else None
            l2 = l2.next if l2 != None else None
            if (l1 == None and l2 == None and carry == 0):
                break
            val1 = l1.val if l1 != None else 0
            val2 = l2.val if l2 != None else 0
            sumOfVal = val1 + val2 + carry
            carry = 1 if sumOfVal > 9 else 0
            tmpNode = ListNode(sumOfVal % 10)
            startNode.next = tmpNode
            startNode = startNode.next

        return mainNode
        